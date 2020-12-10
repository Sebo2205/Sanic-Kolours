﻿using UnityEngine;
[ExecuteInEditMode]
public class CameraThing : MonoBehaviour
{
    public Rigidbody target;
    public static CameraThing main;
    public Vector3 offset;
    public float rotationSpeed = 2;
    public Vector3 rotationOffset;
    public float speed = 10;
    float shakeDuration = 0;
    float originalShakeDuration = 0;
    float shakeIntensity = 0;
    Vector3 targetPos;
    public float rSpeed = 180;
    float yRotation = 0;
    public float minX = -60;
    public float maxX = 60;
    [Range(0, 1)]
    public float rotationMultiplier = 1;
    Vector3 currVeloc = Vector3.zero;
    public Vector3 intensity = Vector3.one;
    public Transform xAxis;
    void Start() {
        main = this;
        yRotation = transform.localEulerAngles.y;
    }
    void FixedUpdate()
    {
        yRotation += Input.GetAxis("Camera Horizontal") * Time.deltaTime * rSpeed;
        Vector3 g = new Vector3(0, yRotation, 0);
        currVeloc = Vector3.Lerp(currVeloc, target.velocity, speed * Time.deltaTime);
        Vector3 h = currVeloc;
        h.Scale(intensity);
        targetPos = target.position + h + offset;
        if (shakeDuration > 0) {
            shakeDuration -= Time.deltaTime;
            targetPos += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * (shakeIntensity * (shakeDuration / originalShakeDuration));
        }
        if (shakeDuration <= 0) {
            shakeDuration = 0;
            originalShakeDuration = 0;
            shakeIntensity = 0;
        }
        Quaternion r = Quaternion.Euler(0, yRotation, 0);
        /*xAxis.localRotation = Quaternion.Lerp(xAxis.localRotation, Quaternion.Lerp(Quaternion.identity, Quaternion.Euler((target.transform.localEulerAngles.x + rotationOffset.x), 
            (target.transform.localEulerAngles.y + rotationOffset.y), 
            (target.transform.localEulerAngles.z + rotationOffset.z)), rotationMultiplier), rotationSpeed * Time.deltaTime);*/
        transform.rotation = Quaternion.Lerp(transform.rotation, r, rotationSpeed * Time.deltaTime);
        transform.position = targetPos;
    }
    public void Shake(float duration, float intensity) {
        if (intensity < shakeIntensity) return;
        originalShakeDuration = duration;
        shakeDuration = duration;
        shakeIntensity = intensity;
    }
}

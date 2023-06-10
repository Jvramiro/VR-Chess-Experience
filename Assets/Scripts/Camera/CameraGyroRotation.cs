using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGyroRotation : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion rotation, initialRotation;

    private void Start(){
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro(){
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            initialRotation = Quaternion.Euler(90f, 90f, 0f);
            gyro.enabled = true;
            return true;
        }

        return false;
    }

    private void Update(){
        if (gyroEnabled){
            Quaternion deviceRotation = Input.gyro.attitude;
            Quaternion unityRotation = new Quaternion(deviceRotation.x, deviceRotation.y, -deviceRotation.z, -deviceRotation.w);

            transform.rotation = initialRotation * unityRotation;
        }
    }

}

using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f, verticalRotationLimit = 80f;
    [SerializeField] private bool useGyroscope = true;
    [SerializeField] private float gyroscopeSensitivity = 1f;
    [SerializeField] private bool smoothRotation = true;
    [SerializeField] private float smoothRotationSpeed = 5f, xRotation = 0f, yRotation = 0f;
    private Quaternion targetRotation;

    void Start(){

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (SystemInfo.supportsGyroscope && useGyroscope){
            Input.gyro.enabled = true;
        }
        else{
            useGyroscope = false;
        }
    }

    void Update(){

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float gyroX = 0f;
        float gyroY = 0f;
        if (useGyroscope){
            gyroX = -Input.gyro.rotationRateUnbiased.y * gyroscopeSensitivity;
            gyroY = Input.gyro.rotationRateUnbiased.x * gyroscopeSensitivity;
        }

        xRotation -= (mouseY + gyroX);
        xRotation = Mathf.Clamp(xRotation, -verticalRotationLimit, verticalRotationLimit);

        yRotation += (mouseX + gyroY);

        targetRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
    }

    void FixedUpdate(){
        if (smoothRotation){
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothRotationSpeed * Time.fixedDeltaTime);
        }
        else{
            transform.rotation = targetRotation;
        }
    }
}

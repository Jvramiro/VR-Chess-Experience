using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f, verticalRotationLimit = 80f;
    [SerializeField] private bool useGyroscope = true;
    [SerializeField] private float gyroscopeSensitivity = 1f;
    [SerializeField] private bool smoothRotation = true;
    [SerializeField] private float smoothRotationSpeed = 5f, xRotation = 0f, yRotation = 0f;
    private Quaternion targetRotation;

    void Update(){

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalRotationLimit, verticalRotationLimit);

        yRotation += mouseX;

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

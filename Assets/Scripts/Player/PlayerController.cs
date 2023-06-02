using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraRaycast cameraRaycast;
    [SerializeField] private float smoothSpeed = 5f;

    [SerializeField] private bool isMoving = false;
    private Vector3 targetPosition;
    
    void OnEnable() => cameraRaycast.NextPosition += CallMovement;
    void OnDisable() => cameraRaycast.NextPosition -= CallMovement;
    
    void Update(){
        if(isMoving){
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f){
                isMoving = false;
            }
        }
    }

    void CallMovement(Vector3 NextTargetPosition){
        if(!isMoving && Vector3.Distance(transform.position, NextTargetPosition) < 2.5f &&
                        Vector3.Distance(transform.position, NextTargetPosition) > 1f){

            targetPosition = new Vector3(NextTargetPosition.x, transform.position.y, NextTargetPosition.z);
            isMoving = true;
        }
    }
}

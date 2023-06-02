using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f;
    private bool isMoving;
    public bool getIsMoving { get{ return isMoving; }}
    private Vector3 targetPosition;

    void Update(){
        if(isMoving){
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f){
                isMoving = false;
            }
        }
    }

    public void CallMovement(Vector3 position){
        if(position == transform.position){ return; }
        
        targetPosition = position;
        isMoving = true;
    }
}

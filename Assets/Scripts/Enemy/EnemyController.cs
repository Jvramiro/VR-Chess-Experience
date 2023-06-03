using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private bool test_directional, test_diagonal, test_noLimit;
    private GameObject player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    Vector3 GetNextMovement(bool directional = false, bool diagonal = false, bool noLimit = false){

        if(!directional && !diagonal){ return transform.position; }

        List<Vector3> nearPositions = GetPawnNearPositions(directional, diagonal, noLimit);
        Vector3 nextPosition = nearPositions.OrderBy(position => Vector3.Distance(position, player.transform.position)).FirstOrDefault();
        
        bool isValid = false;
        while(!isValid){
            if(!CheckPositionIsValid(nextPosition + new Vector3(0,2,0))){
                nearPositions.RemoveAt(0);
                nextPosition = nearPositions.FirstOrDefault();
            }
            else{
                isValid = true;
            }
            if(nearPositions.Count <= 0){ return transform.position; }
        }

        return nextPosition;

    }

    List<Vector3> GetPawnNearPositions(bool directional = false, bool diagonal = false, bool noLimit = false){
        List<Vector3> nearPositions = new List<Vector3>(){};

        if(directional){
            nearPositions.Add(transform.position + new Vector3(2,0,0));
            nearPositions.Add(transform.position + new Vector3(-2,0,0));
            nearPositions.Add(transform.position + new Vector3(0,0,2));
            nearPositions.Add(transform.position + new Vector3(0,0,-2));
        }

        if(diagonal){
            nearPositions.Add(transform.position + new Vector3(2,0,2));
            nearPositions.Add(transform.position + new Vector3(2,0,-2));
            nearPositions.Add(transform.position + new Vector3(-2,0,2));
            nearPositions.Add(transform.position + new Vector3(-2,0,-2));
        }

        if(noLimit && (directional || diagonal)){
            Vector3 bestPosition = nearPositions.OrderBy(position => Vector3.Distance(position, player.transform.position)).FirstOrDefault();
            Vector3 pathSum = transform.position - bestPosition;

            for(int i = 0; i < 7; i++){
                Vector3 addPosition = bestPosition - (pathSum * i);
                nearPositions.Add(addPosition);
            }
        }
        
        return nearPositions;
    }

    Vector3 RaycastDown(Vector3 origin){
        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Selecter")){
                Vector3 hitPosition = hit.collider.gameObject.transform.position;
                return new Vector3(hitPosition.x, transform.position.y, hitPosition.z);
            }
        }

        return transform.position;
    }

    bool CheckPositionIsValid(Vector3 position){
        Ray ray = new Ray(position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Selecter")){
                return true;
            }
        }
        return false;
    }



    [EasyButtons.Button]
    public void TestNextMovement(){
        if(!enemyMovement.getIsMoving){
            enemyMovement.CallMovement(GetNextMovement(test_directional, test_diagonal, test_noLimit));
        }
    }

    public void CallNextMovement(bool directional = false, bool diagonal = false, bool noLimit = false){
        if(!enemyMovement.getIsMoving){
            enemyMovement.CallMovement(GetNextMovement(directional, diagonal, noLimit));
        }
    }

}

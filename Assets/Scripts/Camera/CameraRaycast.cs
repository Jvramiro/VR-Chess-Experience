using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private GameObject selectedObject;
    private PlayController playController;
    private DifficultyController difficultyController;

    public delegate void Vector3Action(Vector3 vector);
    public Vector3Action NextPosition;

    [SerializeField] private float counter, maxDelay = 2f;

    void Update(){
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)){
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Selecter")){
                UpdateSelecter(hit.collider.gameObject);
            }
            else{
                ResetSelecter();
                UpdateCounter(reset: true);
            }

            if (hit.collider.CompareTag("PlayTarget") && Vector3.Distance(transform.position, hit.collider.transform.position) < 2.5f){
                if(playController == null){ playController = hit.collider.GetComponent<PlayController>(); }
                playController.UpdateCounter();
            }
            else{
                if(playController != null){ playController.UpdateCounter(true); }
            }

            if (hit.collider.CompareTag("DifficultyTarget") && Vector3.Distance(transform.position, hit.collider.transform.position) < 2.5f){
                if(difficultyController == null){ difficultyController = hit.collider.GetComponent<DifficultyController>(); }
                difficultyController.UpdateCounter();
            }
            else{
                if(difficultyController != null){ difficultyController.UpdateCounter(true); }
            }

        }
        else{
            ResetSelecter();
            UpdateCounter(reset: true);
            if(playController != null){ playController.UpdateCounter(true); }
            if(difficultyController != null){ difficultyController.UpdateCounter(true); }
        }
    }

    void UpdateSelecter(GameObject selecter){
        if(selecter != selectedObject){
            if(selectedObject != null){ selectedObject.GetComponent<MeshRenderer>().enabled = false; }
            selecter.GetComponent<MeshRenderer>().enabled = true;

            selectedObject = selecter;
            UpdateCounter(reset: true);
        }
        else{
            UpdateCounter();
        }
    }

    void ResetSelecter(){
        if(selectedObject != null){ selectedObject.GetComponent<MeshRenderer>().enabled = false; }
        selectedObject = null;
    }

    public GameObject GetSelectedObject(){
        if(selectedObject != null){
            return selectedObject;
        }
        return null;
    }

    public void UpdateCounter(bool reset = false){
        if(reset){
            counter = 0;
        }
        else if(counter < maxDelay){
            counter += Time.deltaTime;
        }
        else if(counter >= maxDelay){
            counter = 0;
            NextPosition(selectedObject.transform.position);
        }
    }


}

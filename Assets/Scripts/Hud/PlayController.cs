using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    [SerializeField] private float pressDelay = 3;
    [SerializeField] private float counter;
    [SerializeField] private GameObject playTargetFront;

    void Update(){
        playTargetFront.transform.localScale = new Vector3(counterToScale(), counterToScale(), playTargetFront.transform.localScale.z);
    }

    float counterToScale(){
        float percent = counter / pressDelay;
        return 0.35f + (percent * 0.6f);
    }

    public void UpdateCounter(bool reset = false){
        if(!reset && counter < pressDelay){
            counter += Time.deltaTime;
        }
        else if(!reset && counter >= pressDelay){
            counter = 0;
            GameController.Singleton.StartGame();
        }
        else{
            counter = 0;
        }
    }
}

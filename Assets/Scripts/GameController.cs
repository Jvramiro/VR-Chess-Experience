using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    #region Singleton
    public static GameController Singleton;
    void Awake() {
        if (Singleton != null && Singleton != this){
            Destroy(this.gameObject);
        }
        else{
            Singleton = this;
        }
    }
    #endregion
    [SerializeField] private List<GameObject> objectsToHide;
    public void StartGame(){
        CollectableController.Singleton.StartCollectables();
        EnemiesManager.Singleton.StartEnemy(EnemyPiece.pawn);
        foreach(var current in objectsToHide){
            current.SetActive(false);
        }
    }

}

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
    private int difficultyLevel = 0;
    public int getDifficultyLevel { get{ return difficultyLevel; }}

    void Start(){
        difficultyLevel = SaveController.Singleton.GetDifficulty();
    }

    public void StartGame(){
        CollectableController.Singleton.StartCollectables();

        switch(difficultyLevel){
            case 0 : EnemiesManager.Singleton.StartEnemy(EnemyPiece.pawn);
            break;
            case 1 : EnemiesManager.Singleton.StartEnemy(EnemyPiece.bishop_easy);
            break;
            case 2 : EnemiesManager.Singleton.StartEnemy(EnemyPiece.bishop);
                        EnemiesManager.Singleton.UpdateEnemyDelay(3f);
            break;
            case 3 : EnemiesManager.Singleton.StartEnemy(EnemyPiece.rook);
                        EnemiesManager.Singleton.UpdateEnemyDelay(4f);
            break;
        }

        foreach(var current in objectsToHide){
            current.SetActive(false);
        }
    }

    public void UpdateDifficulty(){
        if(difficultyLevel >= 3){
            difficultyLevel = 0;
        }else{
            difficultyLevel++;
        }
    }

}

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
    [SerializeField] private GameObject pawnModel, bishopModel, rookModel;
    [SerializeField] private Collider EnemyCollider;
    private int difficultyLevel = 0;
    public int getDifficultyLevel { get{ return difficultyLevel; }}

    void Start(){
        difficultyLevel = SaveController.Singleton.GetDifficulty();
    }

    public void StartGame(){
        CollectableController.Singleton.StartCollectables();
        EnemyCollider.enabled = true;

        switch(difficultyLevel){
            case 0 : EnemiesManager.Singleton.StartEnemy(EnemyPiece.pawn);
                        pawnModel.SetActive(true);
            break;
            case 1 : EnemiesManager.Singleton.StartEnemy(EnemyPiece.bishop_easy);
                        bishopModel.SetActive(true);
            break;
            case 2 : EnemiesManager.Singleton.StartEnemy(EnemyPiece.bishop);
                        bishopModel.SetActive(true);
                        EnemiesManager.Singleton.UpdateEnemyDelay(3f);
            break;
            case 3 : EnemiesManager.Singleton.StartEnemy(EnemyPiece.rook);
                        rookModel.SetActive(true);
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

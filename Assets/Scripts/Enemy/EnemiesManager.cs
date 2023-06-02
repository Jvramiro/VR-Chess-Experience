using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyPiece{pawn, bishop_easy, bishop, rook}
public class EnemiesManager : MonoBehaviour
{

    #region Singleton
    public static EnemiesManager Singleton;
    void Awake() {
        if (Singleton != null && Singleton != this){
            Destroy(this.gameObject);
        }
        else{
            Singleton = this;
        }
    }
    #endregion

    [SerializeField] private EnemyController enemyController;
    [SerializeField] private bool isActive;
    [SerializeField] private float delay = 2;
    [SerializeField] private float counter;

    [SerializeField] private EnemyPiece enemyPiece = EnemyPiece.pawn;

    void Update(){
        if(isActive && counter < delay){
            counter += Time.deltaTime;
        }
        else if(isActive && counter >= delay){
            counter = 0;
            UpdateEnemy();
        }
        else{
            counter = 0;
        }
    }

    void UpdateEnemy(){
        switch(enemyPiece){
            case EnemyPiece.pawn : enemyController.CallNextMovement(true, false, false);
            break;
            case EnemyPiece.bishop_easy : enemyController.CallNextMovement(false, true, false);
            break;
            case EnemyPiece.bishop : enemyController.CallNextMovement(false, true, true);
            break;
            case EnemyPiece.rook : enemyController.CallNextMovement(true, false, true);
            break;
        }
    }

    public void StartEnemy(EnemyPiece selectedPiece){
        enemyPiece = selectedPiece;
        isActive = true;
    }

    public void StopEnemy(){
        isActive = false;
    }

    public void UpdateEnemyDelay(float delayToUpdate){
        delay = delayToUpdate;
    }
}

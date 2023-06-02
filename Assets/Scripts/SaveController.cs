using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    
    #region Singleton
    public static SaveController Singleton;
    void Awake() {
        if (Singleton != null && Singleton != this){
            Destroy(this.gameObject);
        }
        else{
            Singleton = this;
        }
    }
    #endregion

    public void UpdateScore(int value){
        PlayerPrefs.SetInt("PE_Score", value);
    }

    public int GetScore(){
        return PlayerPrefs.GetInt("PE_Score");
    }

    public void UpdateDifficulty(int value){
        PlayerPrefs.SetInt("PE_Diff", value);
    }

    public int GetDifficulty(){
        return PlayerPrefs.GetInt("PE_Diff");
    }
}

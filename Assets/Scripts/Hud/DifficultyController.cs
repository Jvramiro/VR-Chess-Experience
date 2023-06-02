using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] private float pressDelay = 3;
    [SerializeField] private float counter;
    [SerializeField] private TMP_Text difficultyText;

    void Start(){
        ChangeDifficultyText(SaveController.Singleton.GetDifficulty());
    }

    public void UpdateCounter(bool reset = false){
        if(!reset && counter < pressDelay){
            counter += Time.deltaTime;
        }
        else if(!reset && counter >= pressDelay){
            counter = 0;
            GameController.Singleton.UpdateDifficulty();
            ChangeDifficultyText(GameController.Singleton.getDifficultyLevel);
        }
        else{
            counter = 0;
        }
    }

    private void ChangeDifficultyText(int difficulty){
        switch(difficulty){
            case 0 : difficultyText.text = "Difficulty\nEasy";
            break;
            case 1 : difficultyText.text = "Difficulty\nMedium";
            break;
            case 2 : difficultyText.text = "Difficulty\nHard";
            break;
            case 3 : difficultyText.text = "Difficulty\nRook";
            break;
        }
        SaveController.Singleton.UpdateDifficulty(difficulty);
    }



}

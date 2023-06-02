using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] private float pressDelay = 3;
    [SerializeField] private float counter;
    [SerializeField] private TMP_Text difficultyText;

    public void UpdateCounter(bool reset = false){
        if(!reset && counter < pressDelay){
            counter += Time.deltaTime;
        }
        else if(!reset && counter >= pressDelay){
            counter = 0;
            GameController.Singleton.UpdateDifficulty();
            ChangeDifficultyText();
        }
        else{
            counter = 0;
        }
    }

    private void ChangeDifficultyText(){
        switch(GameController.Singleton.getDifficultyLevel){
            case 0 : difficultyText.text = "Difficulty\nEasy";
            break;
            case 1 : difficultyText.text = "Difficulty\nMedium";
            break;
            case 2 : difficultyText.text = "Difficulty\nHard";
            break;
            case 3 : difficultyText.text = "Difficulty\nRook";
            break;
        }
    }

}

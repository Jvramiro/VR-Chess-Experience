using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CollectableController : MonoBehaviour
{

    #region Singleton
    public static CollectableController Singleton;
    void Awake() {
        if (Singleton != null && Singleton != this){
            Destroy(this.gameObject);
        }
        else{
            Singleton = this;
        }
    }
    #endregion

    [SerializeField] private GameObject collectable;
    [SerializeField] private TMP_Text collectableText;
    [SerializeField] private int collectableCount;

    private List<Vector3> positions = new List<Vector3>();
    private bool isActive;
    void Start(){
        collectableCount = 0;
        FillPositions();
        //Invoke(nameof(StartCollectables), 7f);
        collectableCount = SaveController.Singleton.GetScore();
        collectableText.text = $"Best Score: {collectableCount} Crowns";
    }

    public void StartCollectables(){
        isActive = true;
    }

    void Update(){
        if(isActive){
            var collectables = GameObject.FindGameObjectsWithTag("Collectable");
            if(collectables.Length <= 0){
                SpawnCollectables();
            }
        }
    }

    void SpawnCollectables(){
        if(positions.Count <= 0){ Debug.Log("There is not positions set to collectables"); return; }

        int quantity = Random.Range(1,3);

        for(int i = 0; i < quantity; i++){
            Vector3 position = positions.ElementAt(Random.Range(0, positions.Count));
            GameObject spawnedObject = Instantiate(collectable, position, Quaternion.identity);
        }
    }

    void FillPositions(){
        var getPositions = GameObject.FindGameObjectsWithTag("Selecter");
        if(getPositions.Length <= 0){ Debug.Log("There is not grid positions set to collectables"); return; }

        foreach(var current in getPositions){
            Vector3 positionToAdd = new Vector3(current.transform.position.x, -1f, current.transform.position.z);
            positions.Add(positionToAdd);
        }
    }
    public void IncreaseCollectables(){
        collectableCount++;
        SaveController.Singleton.UpdateScore(collectableCount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if(col.CompareTag("CollectableCollider")){
            Destroy(col.transform.root.gameObject);
            CollectableController.Singleton.IncreaseCollectables();
        }
        if(col.CompareTag("EnemyCollider")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

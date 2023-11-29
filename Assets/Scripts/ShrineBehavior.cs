using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class ShrineBehavior : MonoBehaviour
{
    public GameObject LevelCompleteCanvas;
    public GameObject ObjectivesIncompleteCanvas;

    public int TotalScrollCount { get; private set; }
    public int TotalEnemyCount { get; private set; }

    public PlayerInventory playerInventory;
    public PlayerKills playerKills;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        TotalScrollCount = GameObject.FindGameObjectsWithTag("Scroll").Length;
        TotalEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log(TotalScrollCount);
        Debug.Log(TotalEnemyCount);

        // check for objectives numbers, (# of scrolls, # enemies killed)

        if (other.CompareTag("Player"))
        {
            if(ObjectiveCompleted(playerInventory, playerKills) == true)
            {
                Debug.Log("Level Completed");
                CompleteLevel();
            }
            else
            {
                Debug.Log("Level Not Completed");             
                ObjectivesNotCompleted();
                
            }
           
        }
        
    }

    public void CompleteLevel()
    {
        LevelCompleteCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Play()
    {
        LevelCompleteCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Resume()
    {
        ObjectivesIncompleteCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ObjectivesNotCompleted()
    {
        ObjectivesIncompleteCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public bool ObjectiveCompleted(PlayerInventory playerInventory, PlayerKills playerKills)
    {
        if (playerInventory.NumberScrollsCollected == TotalScrollCount || playerKills.NumberOfEnemyKills == TotalEnemyCount)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}

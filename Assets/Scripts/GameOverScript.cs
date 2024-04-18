using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    //this is the retry button that appears when the game is beated
    public Button RetryButton;
    public TextMeshProUGUI GameOverMessage; //game over message

    //these buttons will be deactivated when the game over screen appears
    public Button boostButton;
    public Button backButton;
    public Button restartButton;
    public Joystick joystick;

    void Start()
    {
        //deactivate the game over message and retry button
        RetryButton.gameObject.SetActive(false);
        GameOverMessage.gameObject.SetActive(false);
        //give the retry button a listener that runs restart scene method when clicked
        RetryButton.onClick.AddListener(RestartScene);
    }
    void Update()
    {
        //constantly check for space and planet trash
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        bool allInactive = true;

        foreach (GameObject trashObject in trashObjects)
        {
            if (trashObject.activeInHierarchy)
            {
                allInactive = false;
                break;
            }
        }

        if (allInactive) //if all trash are collected
        {
            //activate buttons
            RetryButton.gameObject.SetActive(true);
            GameOverMessage.gameObject.SetActive(true);
            //stop time
            Time.timeScale = 0;
            //deactivate unnecessary buttons
            boostButton.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            joystick.gameObject.SetActive(false);
        }
    }

    void RestartScene()
    {
        //restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

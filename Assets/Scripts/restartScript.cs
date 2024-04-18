using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class restartScript : MonoBehaviour
{
    //include the health bar so that it restarts the game when the ship dies
    public Slider healthBar;
    public Button restartButton;

    // Update is called once per frame
    void Start()
    {
        //if the player hits the restart button
        restartButton.onClick.AddListener(RestartScene);
    }
    void Update()
    {
        //if the ship dies or key 'r' is entered
        if(healthBar.value == 0 || Input.GetKey("r"))
        {
            RestartScene();
        }
    }
    void RestartScene()
    {
        //reload and restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

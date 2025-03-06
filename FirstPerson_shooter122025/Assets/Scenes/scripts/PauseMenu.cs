using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseButton;

private bool timepause;
    void Start()
    {
        timepause = false;
    }
    private void TogglePause()
    {
        if(!timepause)
        {
            timepause = true;
            Time.timeScale = 0;
             Cursor.lockState = CursorLockMode.None;
             Cursor.visible = true;
        }
        else
        {
            timepause = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
             Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
        
    }
    //public void Pause()
    //{
    //    PauseButton.SetActive (true);
    //    Time.timeScale = 0;
   // }

    ///public void Continue()
    //{
    //    PauseButton.SetActive(false);
    //    Time.timeScale = 1;
    //}
}

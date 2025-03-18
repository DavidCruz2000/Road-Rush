using UnityEngine;
using UnityEngine.SceneManagement;  // For loading scenes
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    public bool Muzzle = false; 

    void Start()
    {
        Time.timeScale = 0f;
        // Set up button listeners
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    // Start the game
    void StartGame()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");  // Replace with your game scene name
        Time.timeScale = 1f;
        //if (RayShooter != null){RayShooter.Muzzle = true; // Enable the crosshair/target mark when the game starts}

    }

    // Quit the game
    void QuitGame()
    {
        // Quit the game (only works in a built game)
        Application.Quit();

        // In the editor, it will stop playing the scene
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;  // For loading scenes
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    void Start()
    {
        // Set up button listeners
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Start the game
    void StartGame()
    {
        SceneManager.LoadScene("GameScene");  // Replace with your game scene name
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

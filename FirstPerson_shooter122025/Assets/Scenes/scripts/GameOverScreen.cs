using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;  // For loading scenes
using UnityEngine.UI;


public class GameOverScreen : MonoBehaviour
{
    //public Text pointText;
    //public void Setup(int score)
    //{
    //    gameObject.SetActive(true);
    //    pointText.text = score.ToString()+ "  POINTS";
    //}

    public GameObject gameOverUI;  // Drag the game over UI panel here
    public Button restartButton;


    public Button exitButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    // Hide the game over screen initially
        gameOverUI.SetActive(false);

        // Set up button listeners
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);

    }

    public void ShowGameOverScreen()
    {
        // Show the game over UI when the game ends
        gameOverUI.SetActive(true);
    }

    void RestartGame()
    {

        // Unfreeze the game by setting time scale back to 1
        Time.timeScale = 1f;
        
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ExitGame()
    {
        // Quit the game (only works in a built game)
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
    }
}

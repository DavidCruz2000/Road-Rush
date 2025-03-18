using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinSceneController : MonoBehaviour
{
   public GameObject winPanel;   // The win panel to show when the player wins
    //public Text winText;          // The text that will show on the win panel

   public GameObject Inviswall;
   
   public GameObject wonPanel;
   public float winDisplayDuration = 3f; // Time (in seconds) to display the win panel before it disappears

    public Button restartButton;

    public Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        // Initially hide the win panel
        winPanel.SetActive(false);
        // for invisible wall
        Inviswall.SetActive(true); 

        // Initially hide the win panel
        wonPanel.SetActive(false);

        mainMenuButton.onClick.AddListener(GoToMainMenu);
        restartButton.onClick.AddListener(RestartGame);


    }

    // Show the win panel and message
    public void ShowWinPanel()
    {
        // Freeze the game (stop all movement and actions)
        Time.timeScale = 0f;  // Freeze the game time

        // Enable the win panel and set the win message
        winPanel.SetActive(true);
        // to in enable the invisible wall
        Inviswall.SetActive(false); 

        //winText.text = "You Win!";  // Set the message to display on the win panel

        // Optionally, you can use a coroutine to hide the win panel after a few seconds
       StartCoroutine(HideWinPanelAfterDelay(winDisplayDuration)); // Uses the public variable for delay
    }

    // Coroutine to hide the win panel after a delay
    IEnumerator HideWinPanelAfterDelay(float delay)
    {
        // Wait for the specified time
        yield return new WaitForSecondsRealtime(delay); // Use WaitForSecondsRealtime to ignore Time.timeScale

        // Hide the win panel and resume the game
        winPanel.SetActive(false);
        Time.timeScale = 1f;  // Resume the game time
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;    
        
    }

    public void ShowWonPanel()
    {
                // Freeze the game (stop all movement and actions)
        Time.timeScale = 0f;  // Freeze the game time

        // Enable the win panel and set the win message
        wonPanel.SetActive(true);

    }

    public void RestartGame()
{
    // This will reload the current scene (you can also load a specific scene like the main menu)
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

public void GoToMainMenu()
{
    // This will load the main menu scene (you need to have a MainMenu scene in your project)
    SceneManager.LoadScene("MainMenu");
}
}

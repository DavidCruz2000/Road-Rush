//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;
//using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerCharacter : MonoBehaviour
{
    public int maxHealth = 10;    // The player's maximum health
    private int currentHealth;     // The player's current health

 public float maxFuel = 100;      // The player's maximum fuel
    private float currentFuel;       // The player's current fuel

public float fuelDrainRate = 1f; // Fuel drain rate per second while moving


float fuelpermeter = 0.25f;

 public bool canMove = true;    // Flag to control if the player can move
    public FuelBar fuelBar;        // Reference to the FuelBar UI element


    public HealthBar healthBar;//

    public GameOverScreen gameOverScreen;

    public WinSceneController winSceneController;

    


    public bool gameManager; //

    private int kills = 0;          // Track number of kills

    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  // Initialize health at the start
        healthBar.SetMaxHealth(maxHealth);//
        currentFuel = maxFuel;
        fuelBar.SetMaxFuel(maxFuel);        
        // Subscribe to the ENEMY_HIT event
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);      
        Messenger<float>.AddListener("PLAYER_DISTANCE_TRAVELED", OnfuelUsed);
        Time.timeScale = 1f;
    }

        // Method to handle when an enemy is killed
    private void OnEnemyHit()
    {
        kills++;  // Increment the kill count
        Debug.Log("Kills: " + kills);

        if (kills == 10)  // Check if the player has killed 10 enemies
        {
            TriggerWinCondition();  // Trigger the win condition
        }



    }

    // Cleanup listener when the object is disabled
    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    // Method to call to deal damage to the player
    public void Hurt(int damage) 
    {
        currentHealth--;
        healthBar.SetHealth(currentHealth);//
        Debug.Log($"Health: {currentHealth}");
        // Check if health is zero and trigger game over
        if (currentHealth <= 0)
        {
            TriggerGameOver();
        }

    }






////////
///
 private void OnfuelUsed(float DistancetravledDuringFrame)
 {
    DecreaseFuel(DistancetravledDuringFrame * fuelpermeter);
 }
    // Update is called once per frame
    void Update()
    {

                // Decrease fuel when player is moving (this logic can go in FPSInput too if you prefer)
        // (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D))
        //canMove &&
        if ( currentFuel > 0)
        {
            
            //DecreaseFuel(fuelDrainRate*Time.deltaTime);  // Decrease fuel over time when moving
        }

        // Optional: Stop movement if fuel is out
        if (currentFuel <= 0)
        {
            StopMovement();  // Stop player movement when fuel is out
        }

        // Check if health or fuel reaches zero, and trigger game over if either is true
        if (currentHealth <= 0 || currentFuel <= 0)
        {
            TriggerGameOver();
            //gameOverScreen.ShowGameOverScreen();
            StopMovement();  // Stop player movement when fuel is out
            //GameOverScreen.Setup(currentHealth);
            //gameManager.gameOver();
        }

    }
// Method to decrease fuel

//////


public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;  // Prevent health from exceeding max
        }

        Debug.Log($"Health increased! Current health: " + currentHealth);
        healthBar.SetHealth(currentHealth);//
    }



    // Stop player movement
    public void StopMovement()
    {
        canMove = false;  // Disable movement when no fuel is left
        //Time.timeScale = 0f;

    }

    // Resume movement when fuel is refilled (if you plan to refill fuel in the future)
    public void ResumeMovement()
    {
        canMove = true;
        //Time.timeScale = 1f;
    }

    // Check if the player can move
    public bool CanMove()
    {
        return canMove;
    }

    public void DecreaseFuel(float amount)
{
    if (currentFuel > 0)
    {
        currentFuel -= amount;
        fuelBar.SetFuel(currentFuel);  // Update fuel bar

    }
    else
    {
        currentFuel = 0;
        fuelBar.SetFuel(currentFuel);  // Update fuel bar to reflect zero fuel
        StopMovement();  // Stop player movement when fuel is out
    }
}
    private void TriggerGameOver()
    {
        // Freeze the game by setting time scale to 0 (this stops all updates, physics, animations, etc.)
        Time.timeScale = 0f;
        gameOverScreen.ShowGameOverScreen();  // Show the Game Over screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Method to trigger win condition
    private void TriggerWinCondition()
    {
        Time.timeScale = 0f;  // Freeze the game
        Debug.Log("You Win!");
        winSceneController.ShowWinPanel();
        // Optionally, show a "You Win" screen or transition to a new scene
        // You could show a UI element here or transition to the main menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    // Method to refill fuel (can be called from other scripts if needed)
    public void RefillFuel(float amount)
    {
        currentFuel += amount;
        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;  // Ensure fuel doesn't exceed max
        }
        fuelBar.SetFuel(currentFuel);  // Update fuel bar

        // If fuel is refilled, resume movement
        if (currentFuel > 0)
        {
            ResumeMovement();
        }
    }



}

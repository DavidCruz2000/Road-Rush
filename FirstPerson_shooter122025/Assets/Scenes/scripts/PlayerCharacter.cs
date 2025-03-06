using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerCharacter : MonoBehaviour
{
    public int maxHealth = 10;    // The player's maximum health
    private int currentHealth;     // The player's current health

 public int maxFuel = 100;      // The player's maximum fuel
    private int currentFuel;       // The player's current fuel

public float fuelDrainRate = 1f; // Fuel drain rate per second while moving

 public bool canMove = true;    // Flag to control if the player can move
    public FuelBar fuelBar;        // Reference to the FuelBar UI element


    public HealthBar healthBar;//

    public bool gameManager; //

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  // Initialize health at the start
        healthBar.SetMaxHealth(maxHealth);//
        currentFuel = maxFuel;
        fuelBar.SetMaxFuel(maxFuel);                
    }

    // Method to call to deal damage to the player
    public void Hurt(int damage) 
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);//
        Debug.Log($"Health: {currentHealth}");

    }






////////
///
    // Update is called once per frame
    void Update()
    {

        if (currentHealth<0)
        {
            //gameManager.gameOver();
        }
                // Decrease fuel when player is moving (this logic can go in FPSInput too if you prefer)
        // (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D))
        //canMove &&
        if ( currentFuel > 0)
        {
            DecreaseFuel(fuelDrainRate*Time.deltaTime);  // Decrease fuel over time when moving
        }

        // Optional: Stop movement if fuel is out
        if (currentFuel <= 0)
        {
            StopMovement();  // Stop player movement when fuel is out
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
    }

    // Resume movement when fuel is refilled (if you plan to refill fuel in the future)
    public void ResumeMovement()
    {
        canMove = true;
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
        currentFuel -= (int)amount;
        fuelBar.SetFuel(currentFuel);  // Update fuel bar

    }
    else
    {
        currentFuel = 0;
        fuelBar.SetFuel(currentFuel);  // Update fuel bar to reflect zero fuel
        StopMovement();  // Stop player movement when fuel is out
    }
}

    // Method to refill fuel (can be called from other scripts if needed)
    public void RefillFuel(int amount)
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

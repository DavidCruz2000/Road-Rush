using UnityEngine;

using System.Collections;
using System.Collections.Generic;


public class h_pack : MonoBehaviour
{

    public int healthAmount = 5; // Amount of health the health pack restores
    public GameObject player;     // Reference to the player (optional if using tags)


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public GameObject Player;     // Reference to the player (optional if using tags)

    // This will be called when another collider enters the trigger
   private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided is the player
        if (other.CompareTag("Player"))
        {
            // You can call a method to increase the player's health
            // If you have a player health script, increase health here
            PlayerCharacter playerHealth = other.GetComponent<PlayerCharacter>();
            if (playerHealth != null)
            {
                playerHealth.IncreaseHealth(healthAmount); // Increase player health
            }

            // Destroy the health pack after it's been collected
            Destroy(gameObject);  // This will make the health pack disappear
        }
    }
}

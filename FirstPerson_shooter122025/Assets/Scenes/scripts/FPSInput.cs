using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float Normal_speed = 5f;
    public float gravity = -9.8f;
    public float speed;

    public float shift_speed = 15f;//Brandon 





    private CharacterController charController;

    public const float _baseSpeed = 5f;

    private PlayerCharacter playerCharacter; // Reference to the PlayerCharacter script
 public bool canMove = true;    // Flag to control if the player can move



    private void OnEnable() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDisable() {
        Messenger<float>. RemoveListener (GameEvent.SPEED_CHANGED, OnSpeedChanged) ;
        }
    private void OnSpeedChanged (float value) { 
        Normal_speed =_baseSpeed * value;
        }



    void Start()
    {
        charController = GetComponent<CharacterController>();
        playerCharacter = GetComponent<PlayerCharacter>(); // Get the PlayerCharacter script

    }

    // Update is called once per frame
    void Update()
    {



            if (canMove)  // Only allow movement if fuel is not depleted
        {
            Sprint();
            // Get horizontal and vertical movement from player's keyboard input
            //* Time.deltaTime;
            float deltaX = Input.GetAxis ("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            //transform. Translate(deltaX, 0, deltaZ);
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);

            // Clamp magnitude so it moves no faster than the speed
            movement = Vector3.ClampMagnitude (movement, speed);
        
            // Apply gravity
            movement.y = gravity;
            
            // Multiply by time.deltatime so movement is agnostic of framerate
            movement *= Time.deltaTime;
            
            // Transfrom from local coords to global coords
            movement = transform.TransformDirection (movement);

            // Call the character controller's move method and pass in the movement vector.
            charController.Move(movement);
            
            
             // Decrease fuel when player is moving
            if (movement.magnitude > 0)
            {
                playerCharacter.DecreaseFuel(playerCharacter.fuelDrainRate * Time.deltaTime); // Adjust the fuel consumption rate here
            }

        }

    }
// Brandon-------------------------------------------------------------------------
        void Sprint() {
        //It is trggered when not crouching when holding down shift key on the left
        if (Input.GetKeyDown(KeyCode.LeftShift) ) {

            speed = shift_speed;

        }
        //Moves at regular speed when not holding left shift
        if (Input.GetKeyUp(KeyCode.LeftShift) ) {

            speed = Normal_speed;
        }
    }
//----------------------------------------------------------------------------------
 

}

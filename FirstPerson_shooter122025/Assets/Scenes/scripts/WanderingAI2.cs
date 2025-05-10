using UnityEngine;

public class WonderingAI2 : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    public float speed = 3f;
    public float obstacleRange = 5f;
    public float detectionRange = 10f; // The distance at which the AI detects the player
    public float rotationSpeed = 5f;  // Speed at which the AI rotates to follow the player
    public float fieldOfViewAngle = 110f; // The AI's field of view for detecting the player

    private bool isAlive;
    private bool playerSpotted = false;
    private bool isWandering = true;
    private Transform player; // Reference to the player object
    private float lostSightTimer = 0f;
    private float lostSightTime = 3f; // Time before the AI starts wandering again after losing sight of the player
    public const float _baseSpeed = 3f;



    private void OnEnable() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDisable() {
        Messenger<float>. RemoveListener (GameEvent.SPEED_CHANGED, OnSpeedChanged) ;
        }
    private void OnSpeedChanged (float value) { 
        speed =_baseSpeed * value;
        }

        
    private void Start()
    {
        isAlive = true; // Ensure the AI is alive when the game starts
    }

    void Update()
    {
        // Main update logic for the AI when it's alive
        if (isAlive)
        {
            // If the AI is still wandering, it will move forward
            if (isWandering)
            {
                Wander();
            }

            // If the player is spotted, the AI will follow the player
            if (playerSpotted)
            {
                FollowPlayer();
            }
        }
    }

    // Handles the wandering behavior of the AI when it is not following the player
    void Wander()
    {
        // The AI moves forward if it's wandering
        transform.Translate(0, 0, speed * Time.deltaTime);

        // Create a ray to detect objects in front of the AI
        Ray ray = new Ray(transform.position, transform.forward);

        // Perform a sphere cast to check for nearby obstacles or players
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit, detectionRange))
        {
            GameObject hitObject = hit.transform.gameObject;

            // If the AI hits the player, start following the player
            if (hitObject.CompareTag("Player"))
            {
                player = hitObject.transform;  // Store the player's reference
                playerSpotted = true; // Set the player as spotted
                isWandering = false;  // Stop wandering and start following the player
                lostSightTimer = 0f;  // Reset the lost sight timer
            }
            // If the AI detects an obstacle, it will randomly change direction
            else if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0); // Randomly rotate to avoid obstacles
            }
        }
    }

    // Handles the behavior when the AI is following the player
    void FollowPlayer()
    {
        // Ensure that the player is still valid and in range
        if (player != null)
        {
            // Calculate the direction to the player and ignore the vertical axis for rotation
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0;

            // Check if the player is within the AI's field of view angle
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < fieldOfViewAngle / 2f)
            {
                // Smoothly rotate the AI to face the player
                Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

                // Perform a raycast to check if the AI has a clear line of sight to the player
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, detectionRange))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        // The player is still in sight, reset the lost sight timer
                        lostSightTimer = 0f;

                        // Instantiate the fireball when the player is spotted
                        if (fireball == null)
                        {
                            fireball = Instantiate(fireballPrefab);
                            fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                            fireball.transform.rotation = transform.rotation;
                        }
                    }
                    else
                    {
                        // The player is blocked by an obstacle, start the lost sight timer
                        lostSightTimer += Time.deltaTime;
                        if (lostSightTimer >= lostSightTime)
                        {
                            // If the AI loses sight of the player for too long, stop following and start wandering
                            playerSpotted = false;
                            isWandering = true;
                        }
                    }
                }
            }
            else
            {
                // The player is outside the field of view, so stop following
                lostSightTimer += Time.deltaTime;
                if (lostSightTimer >= lostSightTime)
                {
                    playerSpotted = false;
                    isWandering = true;
                }
            }
        }
        else
        {
            // If the player reference is null, stop following
            playerSpotted = false;
            isWandering = true;
        }
    }

    // A method to control the alive status of the AI
    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}

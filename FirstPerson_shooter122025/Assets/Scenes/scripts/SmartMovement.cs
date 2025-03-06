using UnityEngine;

public class SmartMovement : MonoBehaviour
{

    public enum MovementState 
    {
        PAUSED,         // No movement/rotating allowed
        MOVING,         // Moving forward
        BLOCKED,        // Encountered an obstacle, will soon rotate
        ROTATE_LEFT,    // Rotate left
        ROTATE_RIGHT,   // Rotate right
    }

    public float rotationSpeed = 90f;
    public float movementSpeed = 3f;
    public float minObstacleRange = 3f;
    public float maxObstacleRange = 5f;
    public float sphereCastRadius = 0.9f;

    public float speed = 3f;


    [SerializeField] private MovementState _movementState;

    public const float _baseSpeed = 3f;

    private void OnEnable() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDisable() {
        Messenger<float>.RemoveListener (GameEvent.SPEED_CHANGED, OnSpeedChanged) ;
        }
    private void OnSpeedChanged (float value) { 
        speed = _baseSpeed * value;
        }


    private void Start()
    {
        _movementState = MovementState.MOVING;
        transform. Rotate(0,Random. Range (-180f, 180f), 0);
    }


    void FixedUpdate() 
    {
        if (_movementState == MovementState. MOVING)
        {
            // If the player can move, move
            // After that, look for obstacles
            // If one is found, block
            transform. Translate(0, 0, movementSpeed * Time.fixedDeltaTime);
            float distanceToObstacle = DetectObstacle();
            if (distanceToObstacle < minObstacleRange)
            {
                _movementState = MovementState.BLOCKED;
                //randRotationMultiplier = Random. Range(0.9f, 1.1f);
            }
        }
            else if (_movementState == MovementState.BLOCKED) 
            {
                // If the player has detected an obstacle, it's blocked from moving
                // Decide by coin flip to rotate left or right
                int coin = Random. Range(0, 2);
                if (coin == 0) _movementState = MovementState. ROTATE_LEFT;
                    else _movementState = MovementState. ROTATE_RIGHT;
            }
                else if (_movementState == MovementState. ROTATE_LEFT)
                {
                    // Detect obstacles; if found, rotate
                    // If no obstacles are found, start moving again
                    if (DetectObstacle() < maxObstacleRange) 
                    {
                        transform. Rotate(0, -rotationSpeed * Time.fixedDeltaTime, 0);
                    } 
                        else
                        {
                            _movementState = MovementState.MOVING;
                            //randMovementMultiplier = Random. Range(0.9f, 1.1f);
                        }
                        
                }
                    else if (_movementState == MovementState.ROTATE_RIGHT)
                   {
                        // Detect obstacles; if found, rotate
                        // If no obstacles are found, start moving again
                        if (DetectObstacle() < maxObstacleRange) 
                        {
                            transform. Rotate(0, rotationSpeed * Time.fixedDeltaTime, 0);
                        }
                            else 
                            {
                                _movementState = MovementState.MOVING;
                            }
                    }
                    //randMovementMultiplier = Random. Range(0.9f, 1.1f);

    }
    public void ChangeMovementState(MovementState newState)
    {
        _movementState = newState;
    }

    private float DetectObstacle()
    {
        Ray ray= new Ray (transform. position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, sphereCastRadius, out hit)) {
        return hit.distance;
        } else return -1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;

    public float sphereCastRadius = 1f;
    public float detectionRange = 5f;
    public float fireballCooldown = 0.9f;
    private float _currentCooldown;

    public enum FiringState { PAUSED, ALLOWED_TO_FIRE } 
    private FiringState _firingState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentCooldown = 0f;
        _firingState = FiringState.ALLOWED_TO_FIRE;
        
    }

    public void ChangeFiringState (FiringState newState)
    {
        _firingState = newState;
    }

    // Update is called once per frame

    void Update() 
    {
            if (_firingState == FiringState.ALLOWED_TO_FIRE) 
            {
                // Calculate cooldown, if needed
                if (!Mathf.Approximately(_currentCooldown, 0f))
                {
                    _currentCooldown -= Time.deltaTime;
                    _currentCooldown = Mathf.Max(0f, _currentCooldown);
                }
                // Create a ray in the same direction as the game object's direction of move
                Ray ray = new Ray (transform. position, transform. forward);
                // Perform a sphere cast
                RaycastHit hit;
                if (Physics. SphereCast(ray, sphereCastRadius, out hit)) 
                {
                    // Get a reference to the game object hit by the spherecast
                    GameObject hitObject = hit.transform.gameObject;
                    // If the object hit was the player, shoot a fireball at the player
                    if (hitObject.GetComponent<PlayerCharacter>()) 
                    {
                        if (Mathf. Approximately(_currentCooldown, 0f))
                        {
                            GameObject fireball = Instantiate (fireballPrefab);
                            fireball. transform. position = transform.TransformPoint(Vector3.forward * 1.5f);
                            fireball. transform.rotation = transform.rotation;
                            _currentCooldown = fireballCooldown;
                        }
                    }
                }
            }
    }
}

using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{

    [SerializeField] private ParticleSystem _particles;
public Coroutine deathAnim { private set; get;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alreadyHit= false;
        _particles.enableEmission = false;
    }
private bool alreadyHit;
    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Die() 
    {
        // Rotate the game object as if it fell over
        this.transform.Rotate(-75, 0, 0);
        // Turn on particles
        _particles.enableEmission = true;

        // Wait for a few seconds
        yield return new WaitForSeconds (1.5f);
        // Destry game Object
        Destroy (gameObject);
    }
    public void ReactToHit() 
    {
        if(alreadyHit)return;
        alreadyHit= true;
        // Get reference to wandering AI script
        // Pass in FALSE if such a script is attached
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null) 
        {
            behavior.SetAlive(false);
            //StartCoroutine (Die());
        }
        // Do the same for the SmartMovement script
        SmartMovement smart = GetComponent<SmartMovement> ();
        if (smart != null) smart.ChangeMovementState (SmartMovement.MovementState.PAUSED);

        // Do the same for the FireballShooter script
        FireballShooter shooter = GetComponent<FireballShooter>();
        if (shooter != null) shooter.ChangeFiringState(FireballShooter.FiringState.PAUSED);

        //die
        //if(deathAnim == null){
        StartCoroutine(Die());
        Messenger.Broadcast(GameEvent.ENEMY_HIT);
        //}
    }
}

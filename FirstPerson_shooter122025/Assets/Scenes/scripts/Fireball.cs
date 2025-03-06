using System.Collections;
using System.Collections. Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //public
   public float speed = 10f;
   public int damage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
      transform.Translate(0, 0, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) 
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        // If player is not null, then the fireball has hit the player
        if (player != null)
        {
            Debug.Log("Player hit!");
            player.Hurt(damage);

        }
        // Destroy game object
        Destroy (gameObject);  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;  // For loading scenes
using UnityEngine;
using UnityEngine.EventSystems;


public class RayShooter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Private field; stores a reference to the camera
    private Camera cam;
    
    public bool Muzzle = false;  // Crosshair visibility

//Brandon Lopez for sound
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip hitWallSound;
    [SerializeField] AudioClip  hitEnemySound;
//--------


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;   

        
        // Check the scene name to determine if we should show the crosshair
        if (SceneManager.GetActiveScene().name == "SampleScene")  // Replace "SampleScene" with your actual game scene name
        {
            Muzzle = true;  // Show crosshair in the game scene
        }
        else
        {
            Muzzle = false; // Hide crosshair in the main menu or any other scene
        } 
        
    }

    // OnGUI method; for drawing a crosshair

    private void OnGUI() 
    {
        //check
        if(SceneManager.GetActiveScene().name == "SampleScene" && Muzzle){
        int size = 29;
        float posx = cam.pixelWidth / 2 - size / 4;
        float posy = cam.pixelHeight / 2 - size / 2;
        GUI.Label( new Rect(posx, posy, size, size),"+");
        }

        //if (GUI. Button(new Rect(10,10,180,20), "Click here fore a free iPod!"))
        //{
        //    Debug. Log("Button has been clicked!");
        //}  
        
  }

    // Coroutine
// Place down a sphere at a location, which then disappears after one second O references
private IEnumerator SphereIndicator(Vector3 pos) {
    // Create a new sphere game object
    GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
    // Place sphere at pos passed in
    sphere.transform.position = pos;
    // Wait one second
    yield return new WaitForSeconds (1);
    // Destroy the sphere
    Destroy (sphere);
}
    // Update is called once per frame
    void Update()
    {
        // When the player left-clicks, perform a raycast Get
        if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            // Calculate the center of the screen
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

            // Create a ray whose starting point is the middle of the screen
            Ray ray = cam.ScreenPointToRay (point);

            // Create a raycast object to figure out what was hit
            RaycastHit hit;
            if (Physics. Raycast(ray, out hit)) 
            {
                // For now, print out the coords of where the ray hit
                Debug. Log("Hit: " + hit.point);

                // If the object hit was a reactive target, say that it was hit
                // Otherwise, place down a sphere
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null) 
                {
                    target.ReactToHit();
                    soundSource.PlayOneShot(hitEnemySound);

                   // if (target.deathAnim == null)Messenger. Broadcast(GameEvent.ENEMY_HIT);
                    Debug. Log("Target hit!");
                } 
                else
                {
                    StartCoroutine (SphereIndicator(hit.point));
                    soundSource.PlayOneShot(hitWallSound);//Brandon
                }
            }
        }

    }
}

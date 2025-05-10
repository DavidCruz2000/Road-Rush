using UnityEngine;

public class CarHorn : MonoBehaviour {

    public AudioSource hornPlay;
    public AudioClip hornClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            hornPlay.clip = hornClip;
            hornPlay.Play();
        }
    }
}

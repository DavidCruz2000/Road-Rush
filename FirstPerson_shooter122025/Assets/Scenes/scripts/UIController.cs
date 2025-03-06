
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] SettingsPopup settingsPopup;

    //[SerializeField] SettingsPopup FuelBar;

    //[SerializeField] SettingsPopup HealthBar;


   // public GameObject gameOverUI;//to set our gameover UI
    //public void gameOver()
    //{
    //    gameOverUI.SetActive(true);
    //}
    private int _score;

    private void OnEnable() {
        Messenger.AddListener (GameEvent. ENEMY_HIT, OnEnemyHit);
    }

    private void OnEnemyHit() {
            _score += 1; 
            scoreLabel.text =_score.ToString();
    }
    private void OnDisable(){
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _score = 0; 
        scoreLabel.text = _score.ToString();
        settingsPopup.Close();
        //HealthBar.Open();
    }

    // Update is called once per frame
    void Update()
    {
        //scoreLabel.text = Time.realtimeSinceStartup. ToString();
    }
    public void OnOpenSettings () {
        //Debug. Log("Opening settings...");
        settingsPopup.Open();
        //FuelBar.Close();//<
        //HealthBar.Close();//<
    }
    


}

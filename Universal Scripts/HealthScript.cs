using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    public float health = 100f;

    private CharacterAnimation _charecterAnimationScript;
    private EnemyMovement _enemyMovement;
    

    public bool characterDied;
    public bool is_Player;

    private HUD health_UI;

    bool checkDeath;

    void Awake()
    {
        _charecterAnimationScript = GetComponentInChildren<CharacterAnimation>();
        
        if (is_Player) {
            health_UI = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        }
        else
        {
            _enemyMovement = this.GetComponent<EnemyMovement>();
        }
        
    }

   
    public void ApplyDamage(float damage, bool knockDown)
    {

        if (characterDied && !is_Player)
            _enemyMovement.enabled = false; // if enemy dies, disable its movement

        if (characterDied && is_Player)
            return;

        if (characterDied && _enemyMovement.enabled == false )
        {
            _charecterAnimationScript.Death(); // play death animation
            return;
        }

        health -= damage;

        // display health UI

        if(is_Player)
        {

            health_UI.DisplayHealth(health);
            if (health > 0 && !this.GetComponent<Rigidbody>().isKinematic)
            {  
                  _charecterAnimationScript.Hit(); // if player gets hit and health is grater than 0 then play hit animation
            }
        }

        if (health <= 0f && !is_Player) // For enemy
        {           
            _charecterAnimationScript.Death();
            characterDied = true;
            checkDeath = true;          
            return;
        }

        
        if (is_Player && health <= 0f) {
            characterDied = true;
            _charecterAnimationScript.PlayerDeath();
            HUD.HUDinstance.GameOver();
            DisableEnemies();
            Invoke("InvokeMainMenu", 5f); // Then load main menu after 5 seconds gap
            
        }



      

        if(!is_Player) {

            //This part of scipt is for randomly deciding to weather knockdown enemy when player completes a combo on him(enemy)
            
            if(knockDown && characterDied == false) {

                // when knockdown is true, decide again randomly weather to play knockdown enemy animation or not by using random range
                if (Random.Range(0, 2) > 0) {        
                    _charecterAnimationScript.KnockDown();  
                }

            } else {

                if (Random.Range(0, 3) > 1) {       // If knock down is false, decide again weather to play Hit FX or not 
                    _charecterAnimationScript.Hit();
                }

            }

        } 

    } 

    // this function is called when GroundPound attack is used where knockdown in compulsary hence no randomrange is used
    public void KnockDown()
    {
        if(!characterDied)
        _charecterAnimationScript.KnockDown(); //call knockdown animation from charecteranimationscript
    }

    void InvokeMainMenu()
    {
        GameManager.instance.MainMenu();
    }


    void DisableEnemies()
    {
        GameObject[] Enmy = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);  // if is_player deactivate enemy script

        foreach (GameObject E in Enmy)
        {
            E.GetComponent<EnemyMovement>().enabled = false;
            E.GetComponentInChildren<Animator>().enabled = false;
        }
    }

    void RemoveEnemies()
    {
        if (health <= 0f && !is_Player && checkDeath == true) // For enemy
        {
            Destroy(this.gameObject);
            checkDeath = false;
        }
    }
} 





































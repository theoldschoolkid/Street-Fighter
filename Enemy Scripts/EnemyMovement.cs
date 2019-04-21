using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private CharacterAnimation enemyAnim; //  script

    private Rigidbody myBody;
    public float speed = 5f;

    private Transform playerTarget;

    public float attack_Distance = 1f;      // Minimum distance to be maintained between player and enemy
    public float chase_Player_After_Attack = 1f;

    private float current_Attack_Time;      // Attack time counter
    private float default_Attack_Time = 2f; // Time between attacks

    private bool followPlayer, attackPlayer;

    void Awake() {

        // Store all compenents in variable
        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform; //Retrieve player tag name and find that gameobject
    }

  
    void Start() {

        followPlayer = true;
        current_Attack_Time = default_Attack_Time;
    }

  
    void Update() {
        Attack();
    }

    void FixedUpdate() {
        FollowTarget();
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0); 
        //Restrict rotation along x and z axis, this cannot be done in Rigidbody because the transfom.lookAt overrides it hence using LateUpdate
    }

    void FollowTarget() {
        
        // if we are not supposed to follow the player
        if (!followPlayer)
            return;

        // if distance between player and enemy is greater than  attack_Distance follow the player
        if (Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
        {

            transform.LookAt(playerTarget);
            myBody.velocity = transform.forward * speed;

            if(myBody.velocity.sqrMagnitude != 0) {     // if the object velocity is not equal to zero(if the object is moving then 
                enemyAnim.Walk(true);                   // play walk animation
            }


        }

        else if(Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance)
        {

            myBody.velocity = Vector3.zero;
            enemyAnim.Walk(false);

            followPlayer = false;
            attackPlayer = true;

        }

    } 

    void Attack() {

        // if we should NOT attack the player
        // exit the function
        if (!attackPlayer)
            return;

        current_Attack_Time += Time.deltaTime;

        if(current_Attack_Time > default_Attack_Time) {

            enemyAnim.EnemyAttack(Random.Range(0, 3));  // Play any of the 3 attack animation, check charecterAnim script

            current_Attack_Time = 0f;

        }

        if(Vector3.Distance(transform.position, playerTarget.position) >
                attack_Distance + chase_Player_After_Attack) {

            attackPlayer = false;
            followPlayer = true;
        
        }

    } 

} 




































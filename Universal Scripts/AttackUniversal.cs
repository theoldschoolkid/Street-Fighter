using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to gameobject present on hands and legs of player and enemy known as attack points.
/// These points are only activated(and the tags will be changed) when it is triggered by pressing the respective input(for player) or
/// from behaviour scripts(for enemies). Once the attack animation is played the objects are deacticated and tags are removed.
/// The process is controlled from Charecter Animation Events
/// </summary>


public class AttackUniversal : MonoBehaviour {

    public LayerMask collisionLayer;
    public float radius = 1f;
    public float damage = 2f;

    public bool is_Player, is_Enemy;

    public GameObject hit_FX_Prefab;      //  Hit effect animation
    Vector3 hitFX_Pos;                    // the location of the hit effect

    void Update() {
        DetectCollision();    
    }

    void DetectCollision() {

        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);
       
        // Check if any collsion (whose layers are matched with the layers that are defined in collisionLayer variable) 
        //                      is detected inside the sphere of radius (1f) 

        if (hit.Length > 0) {

            #region Player
            // This part is if the gameobject this script is attached to is a player

            if (is_Player && hit[0].transform.gameObject.GetComponentInChildren<CharacterAnimationDelegate>().GetCollisionValue == true) {

                
                hitFX_Pos = hit[0].transform.position;

                // An offset value is added to hit effect postion as the exact transform.position value will at the charecters feet
                // which will instantiate the hit effect at charecters feet, hence we need to add offset values

                hitFX_Pos.y += 1.3f;

                if (hit[0].transform.forward.x > 0) {       // if facing towards

                    hitFX_Pos.x += 0.3f;

                } else if (hit[0].transform.forward.x < 0) {    // if facing away

                    hitFX_Pos.x -= 0.3f;

                }

                Instantiate(hit_FX_Prefab, hitFX_Pos, Quaternion.identity);
                
                // if it is left arm or left leg(which are the final attack in the combo) then apply damage and make knockdown as "true"
                // enemies are knocked down only when hit by final attack of the combo i.e when you complete the combo

                if (gameObject.CompareTag(Tags.LEFT_ARM_TAG) ||          
                    gameObject.CompareTag(Tags.LEFT_LEG_TAG)) {

                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, true);

                } else { // else just apply damage and dont knockdown the enemy.

                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
                }


            }

            #endregion

            #region Enemy

            if (is_Enemy ) {

              // same as above
                hitFX_Pos = hit[0].transform.position;
                hitFX_Pos.y += 1.3f;

                if (hit[0].transform.forward.x > 0)
                {
                   hitFX_Pos.x += 0.3f;
                }
                else if (hit[0].transform.forward.x < 0)
                {
                    hitFX_Pos.x -= 0.3f;
                }

                Instantiate(hit_FX_Prefab, hitFX_Pos, Quaternion.identity);


                if (hit[0].GetComponent<HealthScript>().health > 0)
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false); //player cant be knocked down hence false
                }

            }

            #endregion

            gameObject.SetActive(false);

        } 


    } 


} 









































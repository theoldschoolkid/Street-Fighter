using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to a Gameobject placed on enemies palm
/// </summary>
public class AttackGroundExplosion : MonoBehaviour {

    public LayerMask collisionLayer;  // Ground and enemy
    public float radius = 1.5f;     //collison sphere radius
    public float damage = 20f;

    public GameObject hit_FX_Prefab;

    void Update() {
        
        DetectCollision();    
    }

    void DetectCollision()
    {
        // Check if any collsion (whose layers are matched with the layers that are defined in collisionLayer variable) 
        //                      is detected inside the sphere of radius 1.5 

        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer); 

        if(hit.Length > 0)
        {
            // the first layer detected will always be the ground
            Vector3 hitFX_Pos = this.transform.position;            
            Instantiate(hit_FX_Prefab, hitFX_Pos, Quaternion.identity);  // play a hit VFX at the collision point, the ground VFX will be played

            // if more than 1 collion is detected then it means enemy layer was detected along with ground layer
            // which implies atleast one or more enemies were inside the attack radius

            if (hit.Length > 1) 
            {
                foreach (Collider obj in hit)
                {
                    if (obj.tag == "Enemy" && obj.transform.gameObject.GetComponentInChildren<CharacterAnimationDelegate>().GetCollisionValue == true)
                    {
                        // apply "damage" amount of damage but knockdown is set as "false" because knockdown anim will be played even if the enemy is already
                        // dead and lying on the ground

                        obj.GetComponent<HealthScript>().ApplyDamage(damage, false); 

                        if (obj.GetComponent<HealthScript>().characterDied == false) // play knockdown animation only if enemy is not dead
                            obj.GetComponent<HealthScript>().KnockDown();
                    }
                }
               
            }



        } 
           
     gameObject.SetActive(false); // Disable the Ground Pound gameobject until it recharges

    } 


    } 










































using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Same as GROUND EXPLOSION attack
/// </summary>

public class RoundHouseKick : MonoBehaviour {

    public LayerMask collisionLayer;
    public float radius = 1.3f;
    public float damage = 20f;
    Vector3 hitFX_Pos;
    public GameObject hit_FX_Prefab;
    Collider[] hit;
    

    void Update() {
        DetectCollision();    
    }

    void DetectCollision()
        {
            
            
                hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);

                foreach (Collider obj in hit)
                {
                   
                    if (obj.tag == "Enemy" && obj.transform.gameObject.GetComponentInChildren<CharacterAnimationDelegate>().GetCollisionValue == true)
                    {
                        SetFXOffset(obj);
                // apply "damage" amount of damage but knockdown is set as "false" because knockdown anim will be played even if the enemy is already
                // dead and lying on the ground

                obj.GetComponent<HealthScript>().ApplyDamage(damage, false);

                         if (obj.GetComponent<HealthScript>().characterDied == false)
                         {
                                obj.GetComponent<HealthScript>().KnockDown();   // play knockdown animation only if enemy is not dead    
                                Instantiate(hit_FX_Prefab, hitFX_Pos, Quaternion.identity);
                         }
                    }
                }

                gameObject.SetActive(false);

            
        }

    void SetFXOffset(Collider obj)
    {
        hitFX_Pos = obj.transform.position;
        hitFX_Pos.y += 1.3f;

        if (obj.transform.forward.x > 0)
        {
            hitFX_Pos.x += 0.3f;
        }
        else if (obj.transform.forward.x < 0)
        {
            hitFX_Pos.x -= 0.3f;
        }

    }


} 


  










































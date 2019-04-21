using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Most of this scripts functions are called from the charecter animation using EVENTS
/// </summary>
public class CharacterAnimationDelegate : MonoBehaviour {

    // All the attack points attacked to chatrecters body
    public GameObject left_Arm_Attack_Point, right_Arm_Attack_Point,
            left_Leg_Attack_Point, right_Leg_Attack_Point , ground_Pound_Point, round_House_Kick_Point;

    public float stand_Up_Timer = 2f;   //Time taken to standup after getting knocked down(for enemies)

    private CharacterAnimation animationScript;

    private AudioSource audioSource;

    private PlayerMovement player_movement;

    Rigidbody rd2d;

    bool isCollisionActive = true;

    [SerializeField]
    GameObject gunPrefab;

    [SerializeField]
    GameObject bulletPrefab;

    private EnemyMovement enemy_Movement;

    private ShakeCamera shakeCamera;

    void Awake() {

        // Store all the components before game starts

        animationScript = GetComponent<CharacterAnimation>();

        audioSource = GetComponent<AudioSource>();

        if (gameObject.CompareTag(Tags.PLAYER_TAG))     // check if this script is attached to player
        {
            player_movement = GetComponentInParent<PlayerMovement>();
        }
                 
        if (gameObject.CompareTag(Tags.ENEMY_CHILD_TAG)) // check if this script is attached to enemy
        {  
            enemy_Movement = GetComponentInParent<EnemyMovement>();
        }

        shakeCamera = GameObject.FindWithTag(Tags.MAIN_CAMERA_TAG).GetComponent<ShakeCamera>();

        rd2d = this.GetComponentInParent<Rigidbody>();

    }

    // Below functions turn on(setActive)  attack point gameobject when respective attack button is pressed on deactivcate it once the attack is complete
    // which is detected by "PlayerAttack" script for player  and "EnemyMovement" script for enemies

    #region Attacks

    void Left_Arm_Attack_On() {
        left_Arm_Attack_Point.SetActive(true);  
    }

    void Left_Arm_Attack_Off() {
        if(left_Arm_Attack_Point.activeInHierarchy) {
            left_Arm_Attack_Point.SetActive(false);
        }
    }

    void Ground_Pound_On()
    {     
        ground_Pound_Point.SetActive(true);
    }

    void Ground_Pound_Off()
    {
        if (ground_Pound_Point.activeInHierarchy)
        {
            ground_Pound_Point.SetActive(false);
        }
    }

    void Round_House_Kick_On()
    {      
        round_House_Kick_Point.SetActive(true);
    }

    void Round_House_Kick_Off()
    {
        if (round_House_Kick_Point.activeInHierarchy)
        {
            round_House_Kick_Point.SetActive(false);
        }
    }

    void Round_House_Kick_Power()
    {
        rd2d.AddForce(new Vector3(0, 15f, 0), ForceMode.Impulse);        
    }

    void Right_Arm_Attack_On() {
        right_Arm_Attack_Point.SetActive(true);
    }

    void Right_Arm_Attack_Off() {
        if (right_Arm_Attack_Point.activeInHierarchy) {
            right_Arm_Attack_Point.SetActive(false);
        }
    }

    void Left_Leg_Attack_On() {       
        left_Leg_Attack_Point.SetActive(true);
    }

    void Left_Leg_Attack_Off() {
        if(left_Leg_Attack_Point.activeInHierarchy) {
            left_Leg_Attack_Point.SetActive(false);
        }
    }

    void Right_Leg_Attack_On() {
        right_Leg_Attack_Point.SetActive(true);
    }

    void Right_Leg_Attack_Off() {
        if (right_Leg_Attack_Point.activeInHierarchy) {
            right_Leg_Attack_Point.SetActive(false);
        }
    }

    // Tagging and untagging is done to determine which hand or leg is used for attacking(for determining combos and knockdown)
    void TagLeft_Arm() {                                    
        left_Arm_Attack_Point.tag = Tags.LEFT_ARM_TAG;
    }

    void UnTagLeft_Arm() {
        left_Arm_Attack_Point.tag = Tags.UNTAGGED_TAG;
    }

    void TagLeft_Leg() {
        left_Leg_Attack_Point.tag = Tags.LEFT_LEG_TAG;
    }

    void UnTagLeft_Leg() {
        left_Leg_Attack_Point.tag = Tags.UNTAGGED_TAG;
    }

    #endregion


    #region Audio

    // Attacks respective Audio clips
    void Attack_FX_Sound() {
        AudioManager.Play(AudioName.Attack);
        
    }

     void GroundHit_FX_Sound()
    {
        AudioManager.Play(AudioName.HitGround);

    }

    void EnemyDeathSound() {
        AudioManager.Play(AudioName.EnemyDeath);
    }

    void PlayerDeathSound()
    {
        AudioManager.Play(AudioName.PlayerDeath);
    }

    void Enemy_KnockedDown() {
        AudioManager.Play(AudioName.EnemyFall);
    }

    void Enemy_HitGround() {
        AudioManager.Play(AudioName.HitGround);
    }

    #endregion

    void Enemy_StandUp()
    {
        StartCoroutine(StandUpAfterTime()); // wait for 2 seconds after knowckdown and then standup
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(stand_Up_Timer);
        animationScript.StandUp();
    }

// Enemy movement restrictions
    void DisableMovement() {    // Movement is disabled when enemy gets knocked down
        enemy_Movement.enabled = false;
     
        // Disable collision between 9-player 10-enemy 12-attack points
        Physics.IgnoreLayerCollision(9, 10, true);  // so that player cannot attack enemy when enemy is knocked down
        Physics.IgnoreLayerCollision(12, 10, true); 
        Physics.IgnoreLayerCollision(10, 10, true); // so that other enemies cannot collied with this when this enemy is knocked down
        isCollisionActive = false;

        Invoke("EnableMovemet", 5f);
    }

    void EnableMovemet() {
        enemy_Movement.enabled = true;

        Physics.IgnoreLayerCollision(9, 10, false);
        Physics.IgnoreLayerCollision(12, 10, false);
        Physics.IgnoreLayerCollision(10, 10, false);
        isCollisionActive = true;

    }

    public bool GetCollisionValue
    {
        get { return isCollisionActive;  }
    }

    void ShakeCameraOnFall() {
        shakeCamera.ShouldShake = true;
    }

   // when enemy dies
   void CharacterDied() {
        Physics.IgnoreLayerCollision(9, 10, true);
        Physics.IgnoreLayerCollision(12, 10, true);
        Physics.IgnoreLayerCollision(10, 10, true);
         
        isCollisionActive = false;
        Invoke("DeactivateGameObject", 2f);
    }

    void DeactivateGameObject()
    {
        Destroy(transform.parent.gameObject);
        GameManager.instance.RefreshLevel();
    }

    //when player dies
    void PlayerDied() 
    {
        Physics.IgnoreLayerCollision(9, 10, true);
        Physics.IgnoreLayerCollision(12, 10, true);
        Physics.IgnoreLayerCollision(10, 10, true);
        isCollisionActive = false;
        this.GetComponentInParent<PlayerMovement>().enabled = false;
        this.GetComponentInParent<PlayerAttack>().enabled = false;
        rd2d.constraints = RigidbodyConstraints.FreezePositionX;
        rd2d.constraints = RigidbodyConstraints.FreezePositionZ;

    }
  
    // disable payer movement during punching kicking and after landing from jump
    void DisablePlayerMovement()
    {      
        player_movement.FreezePlayerMovement();
    }

    void EnablePlayerMovement()
    {
        player_movement.UnfreezePlayerMovement();
    }

    // Not used
    void TurnOnGun()
    {
        gunPrefab.SetActive(true);
    }

    void FireBullet()
    {
        GameObject bu =  Instantiate(bulletPrefab) as GameObject;
        bu.transform.position = gunPrefab.transform.position;
        bu.GetComponent<Rigidbody>().AddForce(transform.forward * 70, ForceMode.Impulse);
    }

    void TurnOffGun()
    {
        gunPrefab.SetActive(false);
    }

} 







































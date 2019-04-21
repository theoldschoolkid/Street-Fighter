using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterAnimation player_Anim;
    private Rigidbody myBody;

    public float walk_Speed = 2f;
    public float z_Speed = 1.5f;

     float rotation_Y = -90f;
     float rotation_Speed = 15f;
     float jump_force = 15f;

     bool isGrounded = true;
     bool canJump = true;
     float jumpRayDistance = 1.1f;

     Vector3 rayCastOriginPoint;

   
    public LayerMask whatIsGround;

    
    void Awake() {
        myBody = GetComponent<Rigidbody>();
        player_Anim = GetComponentInChildren<CharacterAnimation>();
    }

    
    void Update() {
        RotatePlayer();
        AnimatePlayerWalk();
        JumpPlayer();        
    }

    void FixedUpdate() {
       
        DetectMovement();
        checkIfGrounded();
    }

    void DetectMovement() {

        myBody.velocity = new Vector3(
            Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (-walk_Speed),
            myBody.velocity.y,
            Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (-z_Speed));

    } 

    void RotatePlayer() {

        if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0) {

            transform.rotation = Quaternion.Euler(0f, rotation_Y, 0f);

        } else if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0) {

            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotation_Y), 0f);

        }

    } 

    void AnimatePlayerWalk() {

        if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) != 0 ||
                Input.GetAxisRaw(Axis.VERTICAL_AXIS) != 0) {

            player_Anim.Walk(true);

        } else {
            player_Anim.Walk(false);
        }

    } 

    void JumpPlayer()
    {

        if (!Input.GetButtonDown("Jump"))
        {
            return;
        }

        if (isGrounded == true && canJump == true)
         {
                canJump = false;
                myBody.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
                player_Anim.Jump();
                isGrounded = false;

         }
        
       
    }

   public void FreezePlayerMovement()
    {
        myBody.isKinematic = true;
    }

   public void UnfreezePlayerMovement()
    {
        myBody.isKinematic = false;       
    }

    void checkIfGrounded()
    {

        //Debug.DrawRay(rayCastOriginPoint, Vector3.down * jumpRayDistance, Color.red);
        rayCastOriginPoint = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);

        // if the ray is touching layers defined in whatIsGround
        if (Physics.Raycast(rayCastOriginPoint, Vector3.down, jumpRayDistance, whatIsGround)) 
        {
            isGrounded = true;
            canJump = true;
        }
        else
        {
            isGrounded = false;
            canJump = false;
        }

    }
  
    public bool GetIsGrounded
    {
        get { return isGrounded; }
    }

}


/*  public void JumpKickForce()
    {

        myBody.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
        isGrounded = false;
        
    }*/







































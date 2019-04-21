using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState { 
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3,
    KICK_1,
    KICK_2
}

public class PlayerAttack : MonoBehaviour {

     CharacterAnimation player_Anim;

     bool activateTimerToReset;

     float default_Combo_Timer = 0.4f;
     float current_Combo_Timer;

     float one_Punch_Timer = 1f;
     float current_Timer = 0f;
     bool activatePunchTimerToReset;
     bool canPunch = true;
     PlayerMovement player_movement;

     ComboState current_Combo_State;

     float GroundPunchTimer = 10f;
     float countDownTimer = 0f;
     bool canGroundPunch = true;



    void Awake() {
        player_Anim = GetComponentInChildren<CharacterAnimation>();
        player_movement = GetComponent<PlayerMovement>();

    }

    void Start() {
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = ComboState.NONE;
    }

    // Update is called once per frame
    void Update() {
        ComboAttacks();
        ResetComboState();
        GroundPunch();     
        JumpKick();
        ResetOnePunchTImer();

        // Fire();
        // GroundKick();
    }

    void ComboAttacks() { 

        if(Input.GetKeyDown(KeyCode.Z) && player_movement.GetIsGrounded) {

            if (current_Combo_State == ComboState.PUNCH_3 ||
                current_Combo_State == ComboState.KICK_1 ||
                current_Combo_State == ComboState.KICK_2)
                return;

            current_Combo_State++;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;

            if(current_Combo_State == ComboState.PUNCH_1) {
                player_Anim.Punch_1();
            }

            if (current_Combo_State == ComboState.PUNCH_2) {
                player_Anim.Punch_2();
            }

            if (current_Combo_State == ComboState.PUNCH_3) {
                player_Anim.Punch_3();
            }

        } // if punch

        if (Input.GetKeyDown(KeyCode.X) && player_movement.GetIsGrounded) {

            // if the current combo is punch 3 or kick 2
            // return meaning exit because we have no combos to perform
            if (current_Combo_State == ComboState.KICK_2 ||
                current_Combo_State == ComboState.PUNCH_3)
                return;

            // if the current combo state is NONE, or punch1 or punch2
            // then we can set current combo state to kick 1 to chain the combo
            if(current_Combo_State == ComboState.NONE ||
                current_Combo_State == ComboState.PUNCH_1 ||
                current_Combo_State == ComboState.PUNCH_2)
            {
                current_Combo_State = ComboState.KICK_1;
            }
            else if(current_Combo_State == ComboState.KICK_1) {
                // move to kick2
                current_Combo_State++;
            }

            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;

            if(current_Combo_State == ComboState.KICK_1) {
                player_Anim.Kick_1();
            }

            if (current_Combo_State == ComboState.KICK_2) {
                player_Anim.Kick_2();
            }


        } // if kick



    } // combo attacks

    void ResetComboState() { 
    
        if(activateTimerToReset) {

            current_Combo_Timer -= Time.deltaTime;

            if(current_Combo_Timer <= 0f) {

                current_Combo_State = ComboState.NONE;

                activateTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;

            }

        }

    } // reset combo state

    void GroundPunch()
    {
        if (canGroundPunch)
        {           
           if (Input.GetKeyDown(KeyCode.Q) && canPunch == true && player_movement.GetIsGrounded)
            {
                activatePunchTimerToReset = true;
                canGroundPunch = false;               
                player_Anim.GroundPunch();
                HUD.HUDinstance.GroudPunch(false);
            }
        }

        else
        {
            RunTimer();
        }
    }

    void RunTimer()
    {
        if (countDownTimer < GroundPunchTimer)
        {
            countDownTimer += Time.deltaTime;
        }
        else
        {
            HUD.HUDinstance.GroudPunch(true);
            canGroundPunch = true;
            countDownTimer = 0;
        }
    }

    void GroundKick()
    {
        if (Input.GetKeyDown(KeyCode.R) && canPunch == true && player_movement.GetIsGrounded)
        {
            activatePunchTimerToReset = true;
            player_Anim.GroundKick();
            
        }
    }

    void JumpKick()
    {

        if (Input.GetKeyDown(KeyCode.E) && canPunch == true && player_movement.GetIsGrounded)
        {        
                activatePunchTimerToReset = true;
                player_Anim.JumpKick();
            
        }
    }

    void Fire()
    {

        if (Input.GetKeyDown(KeyCode.F) && canPunch == true && player_movement.GetIsGrounded)
        {
            activatePunchTimerToReset = true;
            player_Anim.Fire();

        }
    }


    void ResetOnePunchTImer()
    {
        
        if(activatePunchTimerToReset)
        {
            canPunch = false;
            current_Timer += Time.deltaTime;
            
            if (current_Timer >= one_Punch_Timer)
            {
                canPunch = true;
                activatePunchTimerToReset = false;
                current_Timer = 0f;
            }
        }

    }

} // class

































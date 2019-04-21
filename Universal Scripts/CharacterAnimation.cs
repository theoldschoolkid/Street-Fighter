using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to both player and enemy, the functions are called from CharecterAnimationDelegate
/// </summary>
public class CharacterAnimation : MonoBehaviour {

    private Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    public void Walk(bool move) {
        anim.SetBool(AnimationTags.MOVEMENT, move);
    }

    public void Punch_1() {
        anim.SetTrigger(AnimationTags.PUNCH_1_TRIGGER);
    }

    public void Punch_2() {
        anim.SetTrigger(AnimationTags.PUNCH_2_TRIGGER);
    }

    public void Punch_3() {
        anim.SetTrigger(AnimationTags.PUNCH_3_TRIGGER);
    }

    public void Kick_1() {
        anim.SetTrigger(AnimationTags.KICK_1_TRIGGER);
    }

    public void Kick_2() {
        anim.SetTrigger(AnimationTags.KICK_2_TRIGGER);
    }

    public void Jump()
    {
        anim.SetTrigger(AnimationTags.JUMP_TRIGGER);
    }

    public void GroundPunch()
    {
        anim.SetTrigger(AnimationTags.GROUNDPUNCH_TRIGGER);
    }

    public void GroundKick()
    {
        anim.SetTrigger(AnimationTags.GROUNDKICK_TRIGGER);
    }

    public void JumpKick()
    {
        anim.SetTrigger(AnimationTags.JUMPKICK_TRIGGER);
    }

    public void Fire()
    {
        anim.SetTrigger(AnimationTags.FIRE_TRIGGER);
    }







    // ENEMY ANIMATIONS

    public void EnemyAttack(int attack) { 

        if(attack == 0) {
            anim.SetTrigger(AnimationTags.ATTACK_1_TRIGGER);
        }

        if (attack == 1) {
            anim.SetTrigger(AnimationTags.ATTACK_2_TRIGGER);
        }

        if (attack == 2) {
            anim.SetTrigger(AnimationTags.ATTACK_3_TRIGGER);
        }

    } // enemy attack

    public void Play_IdleAnimaiton() {
        anim.Play(AnimationTags.IDLE_ANIMATION);
    }

    public void KnockDown() {
        anim.SetTrigger(AnimationTags.KNOCK_DOWN_TRIGGER);
    }

    public void StandUp() {
        anim.SetTrigger(AnimationTags.STAND_UP_TRIGGER);
    }

    public void Hit() {
        anim.SetTrigger(AnimationTags.HIT_TRIGGER);
    }

    public void Death() {
        anim.SetTrigger(AnimationTags.DEATH_TRIGGER);
    }

    public void PlayerDeath()
    {
        anim.SetBool(AnimationTags.DEATH_TRIGGER, true);
    }

} 












































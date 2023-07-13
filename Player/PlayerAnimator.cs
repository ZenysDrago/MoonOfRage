using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private PlayerController playerControl;

    private void Start()
    {
        playerControl = GetComponent<PlayerController>();
    }

    //              Movement
    public void LaunchMoveAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) >= 0.45 || Mathf.Abs(direction.y) >= 0.45)
        {
            anim.SetBool("Running", true);
            anim.SetBool("Walking", false);
        }
        else if (direction.magnitude > 0)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
        }
        else
        {
            anim.SetBool("Running", false);
            anim.SetBool("Walking", false);
        }
    }

    //              Boolean
    public void SetGrounded(bool isGrounded)
    {
        anim.SetBool("IsGrounded", isGrounded);
    }

    public void SetLink(bool isLink)
    {
        anim.SetBool("Link_Hold", isLink);
    }

    public void SetPush(bool isPushing)
    {
        if (isPushing)
            anim.SetTrigger("StartPushing");

        anim.SetBool("Push", isPushing);
    }

    //===================================
    //              Trigger


    public void StartTriggerJump()
    {
        anim.SetTrigger("Jump");
    }

    public void StartTriggerRoll()
    {
        anim.SetTrigger("Roll");
    }

    public void StartTriggerGetUp()
    {
        anim.SetTrigger("GetUp");
    }


    //              Attack
    public void StartTriggerStomp()
    {
        anim.SetTrigger("Stomp");
    }

    public void StartTriggerQuickAttack()
    {
        anim.SetTrigger("Jab_1");
    }

    public void StartTriggerJAB2()
    {
        anim.SetTrigger("Jab_2");
    }

    public void StartTriggerJAB3()
    {
        anim.SetTrigger("Jab_3");
    }

    public void StartTriggerStrongAttack()
    {
        anim.SetTrigger("Strong_Attack");
    }


    public void StartTriggerUppercut()
    {
        anim.SetTrigger("Uppercut");
    }


    //              Moon Control

    public void StartTriggerRockCall()
    {
        anim.SetTrigger("Rock_Call");
    }

    public void StartTriggerSlam()
    {
        anim.SetTrigger("Rock_Slam");
    }

    public void StartTriggerCallSlam()
    {
        anim.SetTrigger("Rock_CallSlam");
    }


    //          Player Interaction

    public void StartTriggerMediumHit()
    {
        anim.SetTrigger("MediumHitDmg");
    }

    public void StartTriggerMajorHit()
    {
        anim.SetTrigger("MajorHitDmg");
    }


    public void StartTriggerDeath()
    {
        anim.SetTrigger("Death");
    }


    //      Reset Any Trigger
    public void ResetTrigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
    }
}

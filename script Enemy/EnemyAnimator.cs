using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator anim;

    public void Att1()
    {
        anim.SetTrigger("Attack");
    }

    public void AttMinor(bool at2)
    {
        if (at2)
            anim.SetTrigger("Attack_1");
        else
            anim.SetTrigger("Attack_2");
    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }

    public void BigHit()
    {
        anim.SetTrigger("Big_Hit");
    }

    public void Run(bool isRunning)
    {
        anim.SetBool("Running", isRunning);
    }

    public void Grounded(bool isGrounded)
    {
        anim.SetBool("IsGrounded", isGrounded);
    }
}

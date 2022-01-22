using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void CheckForWalkAnim(Rigidbody2D rb)
    {
        if(Mathf.Abs(rb.velocity.magnitude) > 0.3f)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    public void Attack()
    {
        anim.SetTrigger("IdleAttack");
    }
}

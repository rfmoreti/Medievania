using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }

    public void JumpedOn()
    {
        animator.SetTrigger("death");
    }
}

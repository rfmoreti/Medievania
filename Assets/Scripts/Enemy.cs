using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected AudioSource death;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        death = GetComponent<AudioSource>();
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }

    public virtual void JumpedOn()
    {
        death.Play();
        animator.SetTrigger("death");
    }
}

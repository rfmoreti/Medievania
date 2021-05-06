using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletionController : Enemy
{
    private enum State { idle, walking, jumping, falling, hurt }
    private State state = State.idle;

    private SpriteRenderer sr;

    [SerializeField] private float rightWP;
    [SerializeField] private float leftWP;
    [SerializeField] private float speed = 2;


    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        AnimationState();
        animator.SetInteger("state", (int)state);
    }


    private void Movement()
    {
        if (!sr.flipX)
        {
            if (transform.position.x > leftWP)
                TurnAndWalk(-speed);
            else
                rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            if (transform.position.x < rightWP)
                TurnAndWalk(speed);
            else
                rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    private void AnimationState()
    {
        if (Mathf.Abs(rb.velocity.x) > 1)
        {
            state = State.walking;
        }
        else
        {
            state = State.idle;
        }
    }

    private void TurnAndWalk(float velociade)
    {
        state = State.idle;
        
        sr.flipX = !sr.flipX;
        rb.velocity = new Vector2(velociade, rb.velocity.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private Collider2D col2d;


    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private int goldCoins = 0;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private float hurtForce = 3;
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private AudioSource pickCoin;
    [SerializeField] private AudioSource hurtSound;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        col2d = GetComponent<Collider2D>();

        coinsText.text = goldCoins.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }
        

        AnimationState();
        animator.SetInteger("state", (int)state);
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        // Movimentação 
        // Direita
        if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            sprite.flipX = false;
        }

        //Esquerda
        else if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            sprite.flipX = true;
        }

        
        //Pulo
        if (Input.GetButtonDown("Jump") && col2d.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void AnimationState()
    {
       if(state == State.jumping)
        {
            if(rb.velocity.y < 0.1)
            {
                state = State.falling;
            }
        }
       else if(state == State.falling)
        {
            if (col2d.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 0.1)
            {
                state = State.idle;
            }
        }

        else if(Mathf.Abs(rb.velocity.x) > 2)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }

    }

    private void Footsteps()
    {
        footsteps.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            pickCoin.Play();
            Destroy(collision.gameObject);
            goldCoins += 1;
            coinsText.text = goldCoins.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                hurtSound.Play();
                state = State.hurt;
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    //Inimigo na direita, receber dano e mover para esquerda
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //Inimigo na esquerda, receber dano e mover para direita
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }

            }
        }
    }
}

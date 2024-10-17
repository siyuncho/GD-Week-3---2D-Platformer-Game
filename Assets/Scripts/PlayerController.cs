using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool canJump = false;
    private bool canDoubleJump = false;
    private int score = 0;
    [SerializeField] int speed = 0;
    [SerializeField] Animator animator;
    
    // Start is called before thes first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = movementVector;
        rb.velocity = new Vector2(speed*movementVector.x,rb.velocity.y);
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        
        animator.SetBool("Walk_Right",!Mathf.Approximately(movementVector.x,0));
        if(!Mathf.Approximately(movementVector.x,0))
        {
            sr.flipX = movementVector.x < 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            canDoubleJump = true;
        }
       //rb.AddForce(new Vector2(0,200));
    }

    void OnJump()
    {
        if(canJump == true && canDoubleJump == true)
        {
            rb.AddForce(new Vector2(0,250));
            canJump = false;
        }
        else if(canJump == false && canDoubleJump == true)
        {
            rb.AddForce(new Vector2(0,250));
            canDoubleJump = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            score++;
            Debug.Log("My Score is: " + score);
        }
    }
}
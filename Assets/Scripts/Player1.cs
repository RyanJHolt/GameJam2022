using System;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 1;
    public float jumpHeight = 2;
    public double JumpWait = 0;
    public int jumps = 2;
    private float maxSpeed = 10;
    private bool touchingwall = false;
    private float dashDistance = 10;
    Rigidbody2D rb;
    CapsuleCollider2D cc;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            if (col.GetContact(0).point.y < transform.position.y - 0.25)
            {
                jumps = 2;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            
            if (col.GetContact(0).point.x <= transform.position.x - 0.25)
            {
                rb.gravityScale = 0;
                touchingwall = true;
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) )
                {
                    rb.AddForce(new Vector2(200, 200), ForceMode2D.Force);
                    jumps += 1;
                }
            }
            if (col.GetContact(0).point.x >= transform.position.x + 0.25)
            {
                rb.gravityScale = 0;
                touchingwall = true;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) )
                {
                    rb.AddForce(new Vector2(-200, 200), ForceMode2D.Force);
                    jumps += 1;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            rb.gravityScale = 1;
            touchingwall = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.AddForce(new Vector2(500, 0));        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.AddForce(new Vector2(-500, 0));
        }
    }


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (rb.velocity.x < maxSpeed)
            {
                rb.AddForce(new Vector2(moveSpeed, 0), ForceMode2D.Force);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (rb.velocity.x > -maxSpeed)
            {
                rb.AddForce(new Vector2(-moveSpeed, 0), ForceMode2D.Force);
            }

        }

        if ( (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) ) && jumps > 0 && JumpWait <= 0 && !touchingwall)
        {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            jumps -= 1;
            JumpWait = 0.5;
        }

        JumpWait -= 1 * Time.deltaTime;
    }
}

using System;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float x;
    public float y;
    public float moveSpeed = 30;
    public float jumpHeight = 2;
    private double _jumpWait;
    public int jumps = 2;
    private const float MaxSpeed = 10;
    private bool _touchingWall;
    private const float Dash = 20;
    private float dashes = 1;
    private double _dashDuration;
    private double _dashCooldown;
    float _oldX;
    Rigidbody2D _rb;

    GameObject lvl;
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        x = _rb.position.x;
        y = _rb.position.y;
        lvl = GameObject.FindGameObjectWithTag("MainLevel");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("Blue"))
        {
            if (col.GetContact(0).point.y < transform.position.y - 0.2)
            {
                jumps = 2;
                dashes = 1;
                
            }
        }

        if (col.gameObject.CompareTag("Level"))
        {
            // _rb.position = new Vector2(x, y);
            lvl.GetComponent<ResetBoth>().resetPlayers();
        }
        
        if (col.gameObject.CompareTag("Player2"))
        {
            jumps = 2;
            dashes = 1;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform")|| col.gameObject.CompareTag("Blue"))
        {
            
            if (col.GetContact(0).point.x <= transform.position.x - 0.2)
            {
                _rb.gravityScale = 0;
                _touchingWall = true;
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) )
                {
                    _rb.AddForce(new Vector2(50, 40), ForceMode2D.Force);
                }
                jumps = 2;
            }
            if (col.GetContact(0).point.x >= transform.position.x + 0.2)
            {
                _rb.gravityScale = 0;
                _touchingWall = true;
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
                {
                    _rb.AddForce(new Vector2(-50, 50), ForceMode2D.Force);
                }
                jumps =2;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Blue"))
        {
            _rb.gravityScale = 1;
            _touchingWall = false;
        }
    }

    void Update()
    {
        if (_rb.velocity.x > 20)
        {
            _rb.velocity = new Vector2(20, _rb.velocity.y);
        }
        if (_rb.velocity.x < -20)
        {
            _rb.velocity = new Vector2(-20, _rb.velocity.y);
        }
        

        if (Input.GetKeyDown(KeyCode.E) && _dashDuration <= 0 && dashes > 0)
        {
            _rb.gravityScale = 0;
            var velocity = _rb.velocity;
            _oldX = velocity.x;
            velocity = new Vector2(Dash, 0);
            _rb.velocity = velocity;
            _dashDuration = 0.5;
            dashes -= 1;

        }
        if (Input.GetKeyDown(KeyCode.Q)&& _dashDuration <= 0 && dashes > 0)
        {
            _rb.gravityScale = 0;
            var velocity = _rb.velocity;
            _oldX = velocity.x;
            velocity = new Vector2(-Dash, 0);
            _rb.velocity = velocity;
            _dashDuration = 0.5;
            dashes -= 1;
        }
        
        if (_dashDuration > 0)
        {
            _dashDuration -= Time.deltaTime;
            if (_dashDuration <= 0)
            {
                _rb.gravityScale = 1;
                _rb.velocity = new Vector2(_oldX, 0);      
            }
            
        }
        
    }


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (_rb.velocity.x < MaxSpeed)
            {
                _rb.AddForce(new Vector2(moveSpeed, 0), ForceMode2D.Force);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (_rb.velocity.x > -MaxSpeed)
            {
                _rb.AddForce(new Vector2(-moveSpeed, 0), ForceMode2D.Force);
            }

        }

        if ( (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) ) && jumps > 0 && _jumpWait <= 0 && !_touchingWall)
        {
            _rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            jumps -= 1;
            _jumpWait = 0.3;
        }

        _jumpWait -= 1 * Time.deltaTime;
    }
}

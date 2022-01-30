using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float moveSpeed = 30;
    public float jumpHeight = 2;
    public float x;
    public float y;
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
        _rb.gravityScale = -1;
        x = _rb.position.x;
        y = _rb.position.y;
        lvl = GameObject.FindGameObjectWithTag("MainLevel");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("Yellow"))
        {
            if (col.GetContact(0).point.y > transform.position.y + 0.2)
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

        if (col.gameObject.CompareTag("Player1"))
        {
            jumps = 2;
            dashes = 1;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }
        
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("Yellow"))
        {
            
            if (col.GetContact(0).point.x <= transform.position.x - 0.2)
            {
                _rb.gravityScale = 0;
                _touchingWall = true;
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.K) )
                {
                    _rb.AddForce(new Vector2(50, -50), ForceMode2D.Force);
                }
                jumps = 2;
            }
            if (col.GetContact(0).point.x >= transform.position.x + 0.2)
            {
                _rb.gravityScale = 0;
                _touchingWall = true;
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.K) )
                {
                    _rb.AddForce(new Vector2(-50, -50), ForceMode2D.Force);
                }
                jumps = 2;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Yellow"))
        {
            _rb.gravityScale = -1;
            _touchingWall = false;
        }
        if (other.gameObject.CompareTag("Level"))
        {
            _rb.position = new Vector2(x, y);
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
        

        if (Input.GetKeyDown(KeyCode.O) && _dashDuration <= 0 && dashes > 0)
        {
            _rb.gravityScale = 0;
            var velocity = _rb.velocity;
            _oldX = velocity.x;
            velocity = new Vector2(Dash, 0);
            _rb.velocity = velocity;
            _dashDuration = 0.5;
            dashes -= 1;

        }
        if (Input.GetKeyDown(KeyCode.U)&& _dashDuration <= 0 && dashes > 0)
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
                _rb.gravityScale = -1;
                _rb.velocity = new Vector2(_oldX, 0);      
            }
            
        }
        
    }


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (_rb.velocity.x < MaxSpeed)
            {
                _rb.AddForce(new Vector2(moveSpeed, 0), ForceMode2D.Force);
            }
        }
        if (Input.GetKey(KeyCode.J))
        {
            if (_rb.velocity.x > -MaxSpeed)
            {
                _rb.AddForce(new Vector2(-moveSpeed, 0), ForceMode2D.Force);
            }

        }

        if ( (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.RightShift) ) && jumps > 0 && _jumpWait <= 0 && !_touchingWall)
        {
            _rb.AddForce(new Vector2(0, -jumpHeight), ForceMode2D.Impulse);
            jumps -= 1;
            _jumpWait = 0.3;
        }

        _jumpWait -= 1 * Time.deltaTime;
    }
}

using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpHeight = 2;
    public float jumps = 2;
    public float jumpWait = 0;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += Vector3.right * (moveSpeed * Time.deltaTime);
            rb.AddForce(new Vector2(moveSpeed, 0), ForceMode2D.Impulse);

        }
        if (Input.GetKey(KeyCode.A))
        {
            // transform.position += Vector3.right * (-moveSpeed * Time.deltaTime);
            rb.AddForce(new Vector2(-moveSpeed, 0), ForceMode2D.Impulse);
            print(Time.deltaTime.ToString());

        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) ) && jumps != 0 && jumpWait == 0)
        {
            // transform.Translate(Vector3.up*jumpHeight*Time.deltaTime);
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            jumps -= 1;
            jumpWait = 60;

        }

        if (jumpWait > 0)
        {
            jumpWait -= 1;
        }

        if(jumps==0) jumps++;
    }
}

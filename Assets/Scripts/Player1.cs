using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpHeight = 2;
    public float jumps = 2;
    public float jumpWait = 0;
    
    
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * (moveSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.right * (-moveSpeed * Time.deltaTime);
            print(Time.deltaTime.ToString());

        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) ) && jumps != 0 && jumpWait == 0)
        {
            transform.Translate(Vector3.up);
            jumps -= 1;
            jumpWait = 60;

        }

        if (jumpWait > 0)
        {
            jumpWait -= 1;
        }
    }
}

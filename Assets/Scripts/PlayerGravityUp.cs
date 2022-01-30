using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityUp : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player1")){
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
        } else if (other.gameObject.CompareTag("Player2")){
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player1;
    public GameObject player2;
    // Update is called once per frame
    void Update()
    {
        var position1 = player1.transform.position;
        var position2 = player2.transform.position;
        var transform1 = transform;
        transform1.position = new Vector3((position1.x + position2.x)/2,(position1.y + position2.y)/2,transform.position.z);

    }
}

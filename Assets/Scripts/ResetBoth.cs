using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoth : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    // Start is called before the first frame update
    void Start()
    {
        pos1 = GameObject.FindGameObjectWithTag("Player1").transform.position;
        pos2 = GameObject.FindGameObjectWithTag("Player2").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetPlayers(){
        GameObject.FindGameObjectWithTag("Player1").transform.position = pos1;
        GameObject.FindGameObjectWithTag("Player2").transform.position = pos2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transport : MonoBehaviour
{
    public GameObject nextPortal;
    public bool lastPortal = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            print("player collision");
            if(nextPortal!=null){
                other.transform.position = new Vector2(nextPortal.transform.position.x, nextPortal.transform.position.y);
            } else {
                //end of level
            }
            if(lastPortal){
                // other.gameObject.transform.position = new Vector3(-13.5900002f,1.27999997f,0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }
    }
}

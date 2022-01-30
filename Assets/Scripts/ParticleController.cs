using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem ps;
    List<ParticleSystem> allPs;
    Vector2 pos;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        allPs = new List<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ParticleSystem i in allPs)
        {
            if (i.isStopped)
            {
                Destroy(i);
                allPs.Remove(i);
            }
        }

        timer -= 100 * Time.deltaTime;
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Platform") ||other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            //get particle system
            if (timer < 0f)
            {
                Debug.Log("Player Collision");
                ParticleSystem go = Instantiate(ps, new Vector2(transform.position.x, transform.position.y-1), Quaternion.identity);
                go.Play();
                allPs.Add(go);
                timer = 100f;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>= 1.0f)
        {
            GameObject.Find("Jack(temp)").GetComponent<Jack>().rope_cool = true;
            GameObject.Find("Jack(temp)").GetComponent<Jack>().rope_throw = false;
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Robot")
        {
            collider.gameObject.GetComponent<Robot>().restriction = true;
            GameObject.Find("Jack(temp)").GetComponent<Jack>().rope_restriction = true;
            collider.gameObject.tag = "robot with restriction";
            GameObject.Find("Jack(temp)").GetComponent<Jack>().rope_target = collider.gameObject;
            Destroy(gameObject);
        }

    }
}

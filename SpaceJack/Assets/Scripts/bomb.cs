using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("bombbomb", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void bombbomb()
    {
        Collider2D[] colliers = Physics2D.OverlapCircleAll(transform.position, 20.0f);
        foreach (Collider2D hit in colliers)
        {
            if (hit.gameObject.tag == "Obstacle")
            {
                Destroy(gameObject);
            }
            if (hit.gameObject.tag == "Robot")
            {
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, (hit.gameObject.transform.position - gameObject.transform.position).normalized, 20.0f);
                //Debug.DrawRay(transform.position, (hit.gameObject.transform.position - gameObject.transform.position).normalized * 20, Color.red, 1.0f);
                if (hit2 == hit)
                {
                    Debug.Log("robot is detected");
                    Vector3 velocity2 = (hit.gameObject.transform.position - gameObject.transform.position).normalized * 100;
                    hit.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(velocity2.x,velocity2.y);
                    hit.gameObject.GetComponent<Robot>().HP -= 99;
                } 
            }
        }
        Destroy(gameObject);
    }
}

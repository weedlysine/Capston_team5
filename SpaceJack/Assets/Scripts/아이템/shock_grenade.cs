using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shock_grenade : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            explode();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        explode();
    }

    void explode()
    {
        Collider2D[] colliers = Physics2D.OverlapCircleAll(transform.position, 12.0f);
        foreach (Collider2D hit in colliers)
        {
            if (hit.gameObject.tag == "Robot")
            {
                hit.GetComponent<Robot>().restriction = true;
            }
        }
        Destroy(gameObject);
    }
}

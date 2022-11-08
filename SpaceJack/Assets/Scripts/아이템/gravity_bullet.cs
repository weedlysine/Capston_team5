using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity_bullet : MonoBehaviour
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
        if(time>=0.3f)
            explode();
        if (time >= 1.5f)
            Destroy(gameObject);
    }
    void explode()
    {
        Collider2D[] colliers = Physics2D.OverlapCircleAll(transform.position, 12.0f);
        foreach (Collider2D hit in colliers)
        {
            if (hit.gameObject.tag == "Robot")
            {
                Vector3 v3 = (gameObject.transform.position - hit.transform.position).normalized*80;
                hit.attachedRigidbody.AddForce(new Vector2(v3.x, v3.y), ForceMode2D.Impulse);
            }
        }
    }
}

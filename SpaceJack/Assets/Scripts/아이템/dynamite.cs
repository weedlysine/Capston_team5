using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamite : MonoBehaviour
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
        if (time >= 1)
        {
            explode();
            Destroy(gameObject);
        }
           
    }

    void explode()
    {
        Quaternion v3rotate = Quaternion.Euler(0f, 0f, 10.0f);
        Vector3 v3 = new Vector3(0f, 1f, 0f);
        for (int i = 0; i < 36; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, v3, 12f);//시작점,방향,거리
            if (hit.transform.tag == "Robot")
            {
                hit.transform.GetComponent<Robot>().HP -= 120;
                hit.transform.GetComponent<Rigidbody2D>().velocity += new Vector2(v3.normalized.x * 50, v3.normalized.y * 50);//넉백
            }
            v3 = v3rotate * v3;
        }
    }
}

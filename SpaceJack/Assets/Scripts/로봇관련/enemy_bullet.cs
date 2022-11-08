using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    public float damage, time,timer;
    public bool explosive;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= timer)
        {
            if (gameObject.tag == "explosive")
            {
                Quaternion v3rotate = Quaternion.Euler(0f, 0f, 10.0f);
                Vector3 v3 = new Vector3(0f, 1f, 0f);
                for (int i = 0; i < 36; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, v3, 0.5f);//시작점,방향,거리
                    if (hit.transform.tag == "Player")
                    {
                        hit.transform.GetComponent<Jack>().Jack_HP -= damage;
                        hit.transform.GetComponent<Rigidbody2D>().velocity += new Vector2(v3.normalized.x * 50, v3.normalized.y * 50);//넉백
                    }
                    v3 = v3rotate * v3;
                }
                Destroy(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(gameObject.tag == "explosive")
        {
            Quaternion v3rotate = Quaternion.Euler(0f, 0f, 10.0f);
            Vector3 v3 = new Vector3(0f, 1f, 0f);
            for(int i = 0; i<36;i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, v3, 0.5f);//시작점,방향,거리
                if(hit.transform.tag == "Player" || hit.transform.tag == "barricade")
                {
                    hit.transform.GetComponent<Jack>().Jack_HP -= damage;
                    hit.transform.GetComponent<Rigidbody2D>().velocity += new Vector2(v3.normalized.x * 50, v3.normalized.y * 50);//넉백
                }
                v3 = v3rotate * v3;
            }
            Destroy(gameObject);
        }
        else
        {
            if (collider.gameObject.tag == "Player" || collider.transform.tag == "barricade")
            {
                collider.gameObject.GetComponent<Jack>().Jack_HP -= damage;
                Destroy(gameObject);
            }
            else if(collider.gameObject.tag == "Obstacle")
            {
                Destroy(gameObject);
            }
        }
    }
}

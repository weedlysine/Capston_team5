using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class madcow : MonoBehaviour
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
        if (time >= 1.21f)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Robot")
        {
            Vector3 going = gameObject.GetComponent<Rigidbody2D>().velocity;
            Vector3 enemy = collision.transform.position - gameObject.transform.position;
            Vector3 left = Quaternion.Euler(0, 0, 90f) * (going.normalized * 80);
            Vector3 right = Quaternion.Euler(0, 0, -90f) * (going.normalized * 80);
            float angle = Mathf.Atan2(enemy.y, enemy.x) * Mathf.Rad2Deg;
            if (angle > 0)
            {
                collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(left.x, left.y), ForceMode2D.Impulse);
                collision.gameObject.GetComponent<Robot>().HP -= 80;
            }
            else
            {
                collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(right.x, right.y), ForceMode2D.Impulse);
                collision.gameObject.GetComponent<Robot>().HP -= 80;
            }
        }
    }
}

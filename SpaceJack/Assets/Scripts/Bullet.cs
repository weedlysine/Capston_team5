using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    float timer = 0.0f;
    Vector3 velocity2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.54f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        velocity2 = gameObject.GetComponent<Rigidbody2D>().velocity.normalized*30;
        if(collider.gameObject.tag == "Obstacle"){
            Destroy(gameObject);
        }
        if (collider.gameObject.tag == "Robot")
        {
            collider.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(velocity2.x, velocity2.y);
            if(collider.gameObject.transform.GetChild(1).tag == "weaponID_8")
                collider.gameObject.GetComponent<Robot>().HP -= (damage-5);
            else
                collider.gameObject.GetComponent<Robot>().HP -= damage;
            Destroy(gameObject);
        }
            
    }
}

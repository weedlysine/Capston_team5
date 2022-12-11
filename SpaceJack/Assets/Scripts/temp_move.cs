using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_move : MonoBehaviour
{
    public float moveSpeed = 6;
    Rigidbody myRigidbody;
    Camera viewCamera;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
        if(transform.rotation.x !=0 || transform.rotation.y != 0 || transform.rotation.z != 0)
        {
            Time.timeScale = 0;
            transform.rotation = Quaternion.identity;
            Time.timeScale = 1;
        }
            
    }

    void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOLO : MonoBehaviour
{
    int hp = 60;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (hp <= 0 || time >= 8.0f)
            Destroy(gameObject);
    }
}

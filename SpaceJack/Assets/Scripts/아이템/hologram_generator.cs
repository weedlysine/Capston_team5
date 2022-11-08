using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hologram_generator : MonoBehaviour
{
    public Sprite HOLO;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>=1.0f)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = HOLO;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<HOLO>().enabled = true;
            this.enabled = false;
        }
    }
}

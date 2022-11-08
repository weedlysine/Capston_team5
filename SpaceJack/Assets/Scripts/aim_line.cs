using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim_line : MonoBehaviour
{

    float weaponRange = 50f;
    LineRenderer laserLine;


    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("붉은선");
            laserLine.SetPosition(0, gameObject.transform.position);
            laserLine.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}

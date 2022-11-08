using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    public GameObject player;
    public float camera_speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        Vector3 c_Pos = player.transform.position; // 플레이어 위치
        c_Pos.z = Camera.main.transform.position.z;
        Vector3 m_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 마우스 위치
        c_Pos.x = (m_Pos.x + 4 * c_Pos.x) / 5f;      // 마우스:플레이어 = 4:1
        c_Pos.y = (m_Pos.y + 2 * c_Pos.y) / 3f;      // 마우스:플레이어 = 2:1
        Camera.main.transform.position += (c_Pos - Camera.main.transform.position) * camera_speed;
    }
}

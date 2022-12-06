using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    public GameObject player;
    public float camera_speed;
    Camera cam;

    private InputManager inputManager = null;

    float min = 2f;
    float max = 3.5f;
    float initialMin;
    float initialMax;

    public (float, float) inputDir { get; set; }


    private void Awake()
    {
        cam = Camera.main;
        gameObject.AddComponent<InputManager>();
        inputManager = gameObject.GetComponent<InputManager>();
        initialMin = min;
        initialMax = max;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam.orthographicSize = min;
    }

    // Update is called once per frame
    void Update()
    {
        inputManager._Update();
        inputDir = inputManager.Get_Input_Direction();
        Move();
        if (Time.timeScale != 0)
        {
            if ((inputDir.Item1 != 0) || (inputDir.Item2 != 0))
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, min, 0.45f * Time.deltaTime);
            }
            else if ((inputDir.Item1 == 0) && (inputDir.Item2 == 0))
            {

                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, max, 0.25f * Time.deltaTime);
            }
        }
    }

    void Move(){
        Vector3 c_Pos = player.transform.position; // 플레이어 위치
        c_Pos.y = Camera.main.transform.position.y;
        Vector3 m_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 마우스 위치
        c_Pos.x = (m_Pos.x + 4 * c_Pos.x) / 5f;      // 마우스:플레이어 = 4:1
        c_Pos.z = (m_Pos.z + 2 * c_Pos.z) / 3f;      // 마우스:플레이어 = 2:1
        Camera.main.transform.position += (c_Pos - Camera.main.transform.position) * camera_speed;
    }
}

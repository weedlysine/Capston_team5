using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public GameObject bullet;
    public GameObject bomb;
    public GameObject rope;
    public GameObject HP_bar;
    public GameObject Q;
    public GameObject W;
    public GameObject E;
    public GameObject R;
    public GameObject CK;//카드키(cardkey)

    GameObject[] robots;
    public float bullet_speed;
    public float speed;
    public GameObject LB6, LB5, LB4, LB3, LB2, LB1, CT;
    public GameObject rope_target, shotstart;
    public float Jack_HP = 200.0f;
    public int armor_HP = 0, damage;
    public bool rope_restriction;


    private float dx;
    private float dy;
    private float restriction_time;
    private int num = 6;
    private float cooltime, rope_time;
    private bool deadeye, bullet_NULL, walkingAnim;

    private float reload_CT = 0;
    private bool reload_cool;
    private float deadeye_CT = 0;
    private bool dead_cool;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Globals.player_pos = gameObject.transform.position;
        Shot();
    }

    void Move()
    {
        dx = 0;
        dy = 0;
        if (Input.GetKey(KeyCode.A))
        {
            dx--;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            dx++;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            dy--;
        }
        if (Input.GetKey(KeyCode.W))
        {
            dy++;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(dx, dy, 0).normalized * speed;
    }

    void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (num == 0)
                bullet_NULL = true;
            if (num > 0)
            {
                switch (num)
                {
                    case 1:
                        LB6.SetActive(false);
                        num -= 1;
                        break;
                    case 2:
                        LB5.SetActive(false);
                        num -= 1;
                        break;
                    case 3:
                        LB4.SetActive(false);
                        num -= 1;
                        break;
                    case 4:
                        LB3.SetActive(false);
                        num -= 1;
                        break;
                    case 5:
                        LB2.SetActive(false);
                        num -= 1;
                        break;
                    case 6:
                        LB1.SetActive(false);
                        num -= 1;
                        break;
                }
                GameObject ob = Instantiate(bullet, gameObject.transform);
                ob.transform.position = shotstart.transform.position;//발사위치 총구로 고정
                Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
                ob.transform.SetParent(null, true);
                Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

                Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouse_p.z = 0;
                mouse_p = mouse_p - shotstart.transform.position;
                Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-3.18f, 3.18f));
                mouse_p = v3rotation * mouse_p;
                mouse_p.Normalize();
                m_rigidbody.velocity = p_velocity + mouse_p * bullet_speed;
                ob.GetComponent<Bullet>().damage = damage;
            }
        }
    }

    void reload()
    {
        CT.SetActive(true);

        cooltime += Time.deltaTime;
        if (cooltime > 2.0f)
        {
            cooltime = 0;
            CT.SetActive(false);
            LB1.SetActive(true); LB2.SetActive(true); LB3.SetActive(true); LB4.SetActive(true); LB5.SetActive(true); LB6.SetActive(true);
            //cooltime = 2.0f;
            num = 6;
            bullet_NULL = false;
        }
    }

    void deadeyeEND()
    {
        speed *= 2;
        deadeye = false;
        dead_cool = true;
    }
}

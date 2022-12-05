using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jack : MonoBehaviour
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
    public GameObject LB6, LB5, LB4, LB3, LB2, LB1,CT;
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

    private float bomb_CT = 0;
    private bool bomb_cool;
    private float rope_CT = 0;
    public bool rope_cool;
    public bool rope_throw;
    private float reload_CT = 0;
    private bool reload_cool;
    private float deadeye_CT = 0;
    private bool dead_cool;

    
    // Start is called before the first frame update
    void Start()
    {
        CT.SetActive(false);
        CK.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Globals.player_pos = gameObject.transform.position;
        Shot();
        /*if (bullet_NULL ==true)
            reload();
        if(Input.GetKeyUp(KeyCode.Z))
        {
            if (bomb_cool == false)
                skill_bomb();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (rope_cool == false && rope_throw == false)
                skill_rope();
            else if(rope_cool == false && rope_throw == true)
                skill_rope_pull();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (num < 6)
            {
                if (rope_cool == false)
                    skill_quickreload();
            }   
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            if (rope_cool == false)
                skill_deadeye();
        }
        if(deadeye == true)
        {
            foreach (GameObject robot in robots)
                robot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); 
        }
        HP_bar.GetComponent<Image>().fillAmount = Jack_HP*0.005f;
        if(bomb_cool == true)
        {
            bomb_CT += Time.deltaTime;
            Q.GetComponent<Image>().fillAmount = bomb_CT / 10;
            if (bomb_CT >= 10)
            {
                bomb_cool = false;
                bomb_CT = 0;
            }
                
        }
        if(rope_cool == true)
        {
            rope_CT += Time.deltaTime;
            W.GetComponent<Image>().fillAmount = rope_CT / 10;
            if (rope_CT >= 10)
            {
                rope_cool = false;
                rope_CT = 0;
            }
                
        }
        if(reload_cool == true)
        {
            reload_CT += Time.deltaTime;
            E.GetComponent<Image>().fillAmount = reload_CT / 10;
            if (reload_CT >= 10)
            {
                reload_cool = false;
                reload_CT = 0;
            }
        }
        if(dead_cool ==true)
        {
            deadeye_CT += Time.deltaTime;
            R.GetComponent<Image>().fillAmount = deadeye_CT / 10;
            if (deadeye_CT >= 10)
            {
                dead_cool = false;
                deadeye_CT = 0;
            } 
        }
        if(rope_restriction == true)//물체에 맞고난후 시간체크
        {
            restriction_time += Time.deltaTime;
            if (restriction_time >= 2.4f)
            {
                rope_cool = true;
                rope_throw = false;
                restriction_time = 0;
                rope_restriction = false;
            }  
        }*/
    }

    void OnTriggerEnter2D(Collider2D collider)  // card
    {
        if (collider.gameObject.tag == "cardkey")
        {
            CK.SetActive(true);
            Destroy(collider.gameObject);
        }
    }

    void Move()
    {
        dx = 0;
        dy = 0;
        if(Input.GetKey(KeyCode.A)){
            dx--;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(Input.GetKey(KeyCode.D)){
            dx++;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(Input.GetKey(KeyCode.S)){
            dy--;
        }
        if(Input.GetKey(KeyCode.W)){
            dy++;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(dx,dy,0).normalized * speed;
        /*if (dx != 0 || dy != 0)
            GameObject.Find("하체 대기").GetComponent<Animator>().SetBool("ismoving", true);
        else if (dx == 0&& dy==0)
            GameObject.Find("하체 대기").GetComponent<Animator>().SetBool("ismoving", false);*/
    }

    void Shot(){
        if(Input.GetMouseButtonDown(0)){
            if (num == 0)
                bullet_NULL = true;
            if (num>0)
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
                Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-3.18f,3.18f));
                mouse_p = v3rotation*mouse_p;
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
        if (cooltime >2.0f)
        {
            cooltime = 0;
            CT.SetActive(false);
            LB1.SetActive(true); LB2.SetActive(true); LB3.SetActive(true); LB4.SetActive(true); LB5.SetActive(true); LB6.SetActive(true);
            //cooltime = 2.0f;
            num = 6;
            bullet_NULL = false;
        }
        CT.GetComponent<Image>().fillAmount = cooltime / 2.0f;
    }

    void skill_bomb()
    {
        GameObject ob1 = Instantiate(bomb, gameObject.transform);
        Rigidbody2D m_rigidbody = ob1.GetComponent<Rigidbody2D>();
        ob1.transform.SetParent(null, true);
        Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(p_velocity);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_p.z = 0;
        mouse_p = mouse_p - gameObject.transform.position;
        mouse_p.Normalize();
        m_rigidbody.velocity = p_velocity + mouse_p * 70;
        bomb_cool = true;
    }

    void skill_rope()
    {
        GameObject ob1 = Instantiate(rope, gameObject.transform);
        Rigidbody2D m_rigidbody = ob1.GetComponent<Rigidbody2D>();
        ob1.transform.SetParent(null, true);
        Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(p_velocity);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_p.z = 0;
        mouse_p = mouse_p - gameObject.transform.position;
        mouse_p.Normalize();
        m_rigidbody.velocity = p_velocity + mouse_p * 70;
        rope_throw = true;
        rope_time += Time.deltaTime;
    }

    void skill_rope_pull()
    {
        if (rope_target == true)
        {
            Vector2 v2 = new Vector2(gameObject.transform.position.x - rope_target.transform.position.x, gameObject.transform.position.y - rope_target.transform.position.y);
            if (v2.SqrMagnitude() <= 1600)
            {
                v2 = v2.normalized * 100;
                rope_target.GetComponent<Robot>().restriction = false;
                rope_target.GetComponent<Rigidbody2D>().velocity += new Vector2(v2.x, v2.y);
                GameObject.FindWithTag("robot with restriction").tag = "Robot";
                rope_target = null;
                rope_throw = false;
                rope_cool = true;
                rope_restriction = false;
            }
        }
        else
        {
            rope_cool = true;
            rope_throw = false;
            rope_restriction = false;
        }
            
    }
    void skill_quickreload()
    {
        num = 6;
        LB6.SetActive(true);
        LB5.SetActive(true);
        LB4.SetActive(true);
        LB3.SetActive(true);
        LB2.SetActive(true);
        LB1.SetActive(true);
        reload_cool = true;
    }
    void skill_deadeye()
    {
        deadeye = true;
        robots = GameObject.FindGameObjectsWithTag("Robot");
        foreach (GameObject R in robots)
            R.GetComponent<Rigidbody2D>().velocity /= 95;
        speed /= 2;
        Invoke("deadeyeEND", 2);
    }
    void deadeyeEND()
    {
        speed *= 2;
        deadeye = false;
        dead_cool = true;
    }

}

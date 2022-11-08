using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_2 : MonoBehaviour
{
    float cool_time;
    SpriteRenderer Color_change;

    public GameObject GO_dynamite;
    public GameObject GO_shock_grenade;
    public GameObject GO_barricade;
    //public GameObject GO_potatochip;
    //public GameObject GO_coke;
    public GameObject GO_hologram_generator;
    public GameObject GO_gravity_bullet;
    public GameObject GO_claymore;
    bool set;
    public GameObject GO_madcow;
    public GameObject GO_teleporter;
    public GameObject GO_lockpick;
    public GameObject shotstart;

    // Start is called before the first frame update
    void Start()
    {
        Color_change = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            dynamite();
        if (Input.GetKeyUp(KeyCode.Alpha2))
            shock_grenade();
        if (Input.GetKeyUp(KeyCode.Alpha3))
            barricade();
        if (Input.GetKeyUp(KeyCode.Alpha4))
            potatochip();
        if (Input.GetKeyUp(KeyCode.Alpha5))
            coke();
        if (Input.GetKeyUp(KeyCode.Alpha6))
            hologram_generator();
        if (Input.GetKeyUp(KeyCode.Alpha7))
            gravity_bullet();
        if (Input.GetKeyUp(KeyCode.Alpha8))
            claymore();
        if (Input.GetKeyUp(KeyCode.Alpha9))
            madcow();
        if (Input.GetKeyUp(KeyCode.Alpha0))
            teleporter();
    }
    void dynamite()//다이너마이트
    {
        //던지기
        GameObject ob1 = Instantiate(GO_dynamite, gameObject.transform);
        ob1.transform.position = shotstart.transform.position;//발사위치 총구로 고정
        Rigidbody2D m_rigidbody = ob1.GetComponent<Rigidbody2D>();
        ob1.transform.SetParent(null, true);
        Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(p_velocity);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_p.z = 0;
        mouse_p = mouse_p - gameObject.transform.position;
        mouse_p.Normalize();
        m_rigidbody.velocity = p_velocity + mouse_p * 50;
        m_rigidbody.angularVelocity = 720;
        cool_time = 14;
    }
    void shock_grenade()//자기충격수류탄
    {
        //던지기
        GameObject ob1 = Instantiate(GO_shock_grenade, gameObject.transform);
        ob1.transform.position = shotstart.transform.position;//발사위치 총구로 고정
        Rigidbody2D m_rigidbody = ob1.GetComponent<Rigidbody2D>();
        ob1.transform.SetParent(null, true);
        Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(p_velocity);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_p.z = 0;
        mouse_p = mouse_p - gameObject.transform.position;
        mouse_p.Normalize();
        m_rigidbody.velocity = p_velocity + mouse_p * 40;
        m_rigidbody.angularVelocity = 720;
        cool_time = 16;
    }
    void barricade()//원터치 바리케이드
    {
        //설치
        GameObject ob = Instantiate(GO_barricade, gameObject.transform);
        Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
        ob.transform.SetParent(null, true);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Avector = mouse_p - transform.localPosition;
        float angle = Mathf.Atan2(Avector.y, Avector.x) * Mathf.Rad2Deg;
        if (angle > -45 && angle < 45)
            ob.transform.position = gameObject.transform.position + new Vector3(5, 0, 0);
        if (angle >= 45 && angle < 135)
        {
            ob.transform.rotation = Quaternion.Euler(0, 0, 90);
            ob.transform.position = gameObject.transform.position + new Vector3(0, 5, 0);
        }
        if (angle >= 135 || angle < -135)
            ob.transform.position = gameObject.transform.position + new Vector3(-5, 0, 0);
        if (angle >= -135 && angle < -45)
        {
            ob.transform.rotation = Quaternion.Euler(0, 0, 90);
            ob.transform.position = gameObject.transform.position + new Vector3(0, -5, 0);
        }
        cool_time = 45;
    }
    void potatochip()//갑자칩
    {
        GameObject.Find("Jack(temp)").GetComponent<Jack>().Jack_HP += 50;
        cool_time = 30;
    }
    void coke()//콜라
    {
        GameObject.Find("Jack(temp)").GetComponent<Jack>().damage += 5;
        StartCoroutine(coke_end());
        cool_time = 30;
    }
    IEnumerator coke_end()
    {
        float time = Time.deltaTime;
        while(time<15.0f)//캐릭터 점멸
        {
            Color_change.color = new Color(214 / 255f, 124 / 255f, 124 / 255f, 1);
            yield return new WaitForSeconds(0.2f);
            Color_change.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
        }
        GameObject.Find("Jack(temp)").GetComponent<Jack>().damage -= 5;
        yield break;
    }
    void hologram_generator()//홀로그램 영사기
    {
        //던지기
        GameObject ob1 = Instantiate(GO_hologram_generator, gameObject.transform);
        ob1.transform.position = shotstart.transform.position;//발사위치 총구로 고정
        Rigidbody2D m_rigidbody = ob1.GetComponent<Rigidbody2D>();
        ob1.transform.SetParent(null, true);
        Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(p_velocity);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_p.z = 0;
        mouse_p = mouse_p - gameObject.transform.position;
        mouse_p.Normalize();
        m_rigidbody.velocity = p_velocity + mouse_p * 36;
        cool_time = 25;
    }
    void gravity_bullet()//중력자탄
    {
        //던지기
        GameObject ob1 = Instantiate(GO_gravity_bullet, gameObject.transform);
        Rigidbody2D m_rigidbody = ob1.GetComponent<Rigidbody2D>();
        ob1.transform.SetParent(null, true);
        Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(p_velocity);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_p.z = 0;
        mouse_p = mouse_p - gameObject.transform.position;
        mouse_p.Normalize();
        m_rigidbody.velocity = p_velocity + mouse_p * 90;
        cool_time = 20;
    }
    void claymore()//클레이모어
    {
        //설치
        GameObject ob = Instantiate(GO_claymore, gameObject.transform);
        Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
        ob.transform.SetParent(null, true);
        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Avector = mouse_p - transform.localPosition;
        float angle = Mathf.Atan2(Avector.y, Avector.x) * Mathf.Rad2Deg;
        ob.transform.position = gameObject.transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(6, 0, 0);
        ob.transform.rotation = Quaternion.Euler(0, 0, angle);
        set = true;
        // 격발
        if(set == true)
        {
            set = false;
            Destroy(ob);
        }
        cool_time = 40;
    }
    void madcow()//메카우
    {
        GameObject ob = Instantiate(GO_madcow, gameObject.transform);
        Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
        ob.transform.SetParent(null, true);
        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Avector = mouse_p - gameObject.transform.position;
        Debug.Log(Avector.normalized);
        float angle = Mathf.Atan2(Avector.y, Avector.x) * Mathf.Rad2Deg;
        ob.transform.position = gameObject.transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(6, 0, 0);
        if (angle > -90 && angle < 90)
        {
            ob.transform.localScale = new Vector3(-1, 1, 1);
            ob.transform.rotation = Quaternion.Euler(0, 0, angle);
            ob.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, angle) * new Vector3(60, 0, 0);
        }
        if(angle<=-90 || angle>=90)
        {
            ob.transform.rotation = Quaternion.Euler(0, 0, angle-180);
            ob.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, angle) * new Vector3(60, 0, 0);
        }
        cool_time = 30;
    }
    void teleporter()//간이텔레포터
    {
        cool_time = 60;
    }
    void lockpick()//락픽
    {
        cool_time = 0;
    }
}

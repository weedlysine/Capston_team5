using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class item : MonoBehaviour
{
    public GameObject Trap;
    public GameObject Smoke_bomb;
    public GameObject Flare;
    public GameObject Barricade;
    public Sprite smoke;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
            barricade();
        if (Input.GetKeyUp(KeyCode.E))
            smoke_bomb();

    }

    void smoke_bomb()//연막탄(던지기)
    {
        GameObject ob = Instantiate(Smoke_bomb, gameObject.transform);
        Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
        ob.transform.SetParent(GameObject.Find("Jack(temp)").transform);
        Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(p_velocity);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_p.z = 0;
        mouse_p = mouse_p - gameObject.transform.position;
        mouse_p.Normalize();
        m_rigidbody.velocity = p_velocity + mouse_p * 70;
        m_rigidbody.angularVelocity =720;
        StartCoroutine(smoke_bomb_explosion(ob));
    }
    IEnumerator smoke_bomb_explosion(GameObject a)//연막탄 폭발후 가스 팽창
    {
        yield return new WaitForSeconds(3.0f);
        a.GetComponent<Rigidbody2D>().angularVelocity = 0;
        a.GetComponent<SpriteRenderer>().sprite = smoke;
        a.GetComponent<SpriteRenderer>().color = new Color(11, 1, 1, 0.5f);
        float time = 1;
        while (time<=2)
        {
            time += Time.deltaTime;
            a.transform.localScale = new Vector3(time*8, time*8,0);
            yield return new WaitForSeconds(0.03f);
        }
        StartCoroutine(smoke_bomb_destroy(a));
    }
    IEnumerator smoke_bomb_destroy(GameObject b)//가스 제거
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(b);
    }
    void GravityGrenade()//중력수류탄(던지기 + 폭발)
    {
        //범위네 적 가운데로 끌어당기기(자이라 궁)
    }
    void flare()//섬광탄(던지기)
    {
        GameObject ob = Instantiate(Flare, gameObject.transform);
        Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
        ob.transform.SetParent(GameObject.Find("Jack(temp)").transform);
        Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(p_velocity);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_p.z = 0;
        mouse_p = mouse_p - gameObject.transform.position;
        mouse_p.Normalize();
        m_rigidbody.velocity = p_velocity + mouse_p * 70;
        m_rigidbody.angularVelocity = 720;
        StartCoroutine(flare_explosion(ob));
    }
    IEnumerator flare_explosion(GameObject a)//섬광탄 폭발
    {
        yield return new WaitForSeconds(3.0f);
        //폭발 이펙트 코드
        Destroy(a);
        Collider2D[] colliers = Physics2D.OverlapCircleAll(transform.position, 20.0f);
        foreach(Collider2D hit in colliers)
        {
            if(hit.gameObject.tag == "human")
            {
                float time = 0;
                while(time <=3)
                {
                    time += Time.deltaTime;
                    //인간형 적 상태이상 코드(인간형적 스크립트안에 stun코드 true로 변경후 인간형 적 스크립트에서 처리)
                    yield return new WaitForSeconds(0.03f);
                }
            }
        }
        
    }
    void trap()//덫(던지기)
    {
        GameObject ob = Instantiate(Trap, gameObject.transform);
        Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
        ob.transform.SetParent(null, true);
        Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(p_velocity);

        Vector3 mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_p.z = 0;
        mouse_p = mouse_p - gameObject.transform.position;
        mouse_p.Normalize();
        m_rigidbody.velocity = p_velocity + mouse_p * 50;
    }
    void speed_up()//이속증가물약(10초뒤 원상복구)
    {
        GameObject.Find("Jack(temp)").GetComponent<Jack>().speed *= 1.3f;
        Invoke("speed_up_return", 10);
    }
    void speed_up_return()//이속원상복귀invoke함수
    {
        GameObject.Find("Jack(temp)").GetComponent<Jack>().speed = 50;
    }
    void healingpack()//치유팩
    {
        if (GameObject.Find("Jack(temp)").GetComponent<Jack>().Jack_HP < 6)
            GameObject.Find("Jack(temp)").GetComponent<Jack>().Jack_HP += 1;
    }
    void armorpack()//아머팩
    {
        GameObject.Find("Jack(temp)").GetComponent<Jack>().armor_HP += 1;
    }
    void barricade()//장애물,바리케이트(생성)
    {
        GameObject ob = Instantiate(Barricade, gameObject.transform);
        Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
        ob.transform.SetParent(GameObject.Find("Jack(temp)").transform);

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
        if(angle >=135 || angle < -135)
            ob.transform.position = gameObject.transform.position + new Vector3(-5, 0, 0);
        if (angle >=-135 && angle < -45)
        {
            ob.transform.rotation = Quaternion.Euler(0, 0, 90);
            ob.transform.position = gameObject.transform.position + new Vector3(0, -5, 0);
        }

    }
    void fluoroscope()//투시경
    {

    }
    void magnetic_field_generator()//자기장발생장치
    {
        Collider2D[] colliers = Physics2D.OverlapCircleAll(transform.position, 20.0f);
        foreach (Collider2D hit in colliers)
        {
            if(hit.gameObject.tag == "drone")
            {
                //드론 작동코드 끄기(복구불가)
                //로봇류 몇초간 코드error(몇초뒤 복구불가)
            }
        }
    }
    void steampack()//스팀팩
    {
        if (GameObject.Find("Jack(temp)").GetComponent<Jack>().Jack_HP > 1)
            GameObject.Find("Jack(temp)").GetComponent<Jack>().Jack_HP -= 1;
        GameObject.Find("Jack(temp)").GetComponent<Jack>().speed *= 1.5f;
        Invoke("speed_up_return", 10);
    }
    void hologram_generator()//홀로그램생성기
    {
        //홀로그램 생성시 적은 홀로그램 먼저 타겟팅
    }
    void healing_device()//체력회복장치
    {
        GameObject.Find("Jack(temp)").GetComponent<Jack>().Jack_HP = 6;
    }
}

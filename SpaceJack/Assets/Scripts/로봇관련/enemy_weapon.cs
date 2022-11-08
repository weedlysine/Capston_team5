using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_weapon : MonoBehaviour
{

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callcoroutine(int i)
    {
        switch(i)
        {
            case 1:
                StartCoroutine(weaponID_1());
                break;
            case 2:
                StartCoroutine(weaponID_2());
                break;
            case 3:
                StartCoroutine(weaponID_3());
                break;
            case 4:
                StartCoroutine(weaponID_4());
                break;
            case 5:
                StartCoroutine(weaponID_5());
                break;
            case 6:
                StartCoroutine(weaponID_6());
                break;
            case 7:
                StartCoroutine(weaponID_7());
                break;
            case 8:
                StartCoroutine(weaponID_8());
                break;
        }
    }
    public IEnumerator weaponID_1()//비살상 미니건
    {
        float[] W_Info = new float[7];
        for (int j = 0; j < 7; j++)
            W_Info[j] = Globals.weapon_Info[0, j];

        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = true;
        yield return new WaitForSeconds(1.5f);
        for(int i=0;i<W_Info[5]; i++)//탄창 수 만큼 반복
        {
            GameObject ob = Instantiate(bullet, gameObject.transform);
            ob.transform.position = gameObject.transform.position;
            Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
            ob.transform.SetParent(null, true);
            ob.GetComponent<enemy_bullet>().damage = W_Info[0];
            ob.GetComponent<enemy_bullet>().timer = W_Info[6];//(데미지, 탄수명)탄에 전달
            //Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

            Vector3 dir = Globals.player_pos;
            //pos.z = 0;
            dir = dir - gameObject.transform.position;
            Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-W_Info[2], W_Info[2]));//집탄도,발사각
            dir = v3rotation * dir;//집탄보정
            dir.Normalize();
            m_rigidbody.velocity = dir * W_Info[3];//탄속보정
            yield return new WaitForSeconds(W_Info[4]);//발사간격
            //Debug.Log(i);
        }
        Debug.Log("탄다씀");
        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = false;
        gameObject.transform.parent.parent.GetComponent<Robot>().Robot_Reload = true;
        gameObject.transform.parent.parent.GetComponent<Robot>().reloadtime = W_Info[1];

    }
    public IEnumerator weaponID_2()// 미니 로켓
    {
        float[] W_Info = new float[7];
        for (int j = 0; j < 7; j++)
            W_Info[j] = Globals.weapon_Info[1, j];

        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = true;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < W_Info[5]; i++)//탄창 수 만큼 반복
        {
            GameObject ob = Instantiate(bullet, gameObject.transform);//탄 오브젝트 생성
            ob.transform.position = gameObject.transform.position;
            ob.tag = "explosive";
            Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
            ob.transform.SetParent(null, true);
            ob.GetComponent<enemy_bullet>().damage = W_Info[0];
            ob.GetComponent<enemy_bullet>().timer = W_Info[6];//(데미지, 탄수명)탄에 전달
            //Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

            Vector3 dir = Globals.player_pos;
            //pos.z = 0;
            dir = dir - gameObject.transform.position;
            Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-W_Info[2], W_Info[2]));//집탄도,발사각
            dir = v3rotation * dir;//집탄보정
            dir.Normalize();
            m_rigidbody.velocity = dir * W_Info[3];//탄속보정
            yield return new WaitForSeconds(W_Info[4]);//발사간격
            //Debug.Log(i);
        }
        Debug.Log("탄다씀");
        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = false;
        gameObject.transform.parent.parent.GetComponent<Robot>().Robot_Reload = true;
        gameObject.transform.parent.parent.GetComponent<Robot>().reloadtime = W_Info[1];
    }
    public IEnumerator weaponID_3()// 자동소총
    {
        float[] W_Info = new float[7];
        for (int j = 0; j < 7; j++)
            W_Info[j] = Globals.weapon_Info[2, j];

        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = true;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < W_Info[5]; i++)//탄창 수 만큼 반복
        {
            GameObject ob = Instantiate(bullet, gameObject.transform);
            ob.transform.position = gameObject.transform.position;
            Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
            ob.transform.SetParent(null, true);
            ob.GetComponent<enemy_bullet>().damage = W_Info[0];
            ob.GetComponent<enemy_bullet>().timer = W_Info[6];//(데미지, 탄수명)탄에 전달
            //Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

            Vector3 dir = Globals.player_pos;
            //pos.z = 0;
            dir = dir - gameObject.transform.position;
            Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-W_Info[2], W_Info[2]));//집탄도,발사각
            dir = v3rotation * dir;//집탄보정
            dir.Normalize();
            m_rigidbody.velocity = dir * W_Info[3];//탄속보정
            yield return new WaitForSeconds(W_Info[4]);//발사간격
            //Debug.Log(i);
        }
        Debug.Log("탄다씀");
        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = false;
        gameObject.transform.parent.parent.GetComponent<Robot>().Robot_Reload = true;
        gameObject.transform.parent.parent.GetComponent<Robot>().reloadtime = W_Info[1];
    }
    public IEnumerator weaponID_4()//자동유탄발사기
    {
        float[] W_Info = new float[7];
        for (int j = 0; j < 7; j++)
            W_Info[j] = Globals.weapon_Info[1, j];

        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = true;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < W_Info[5]; i++)//탄창 수 만큼 반복
        {
            GameObject ob = Instantiate(bullet, gameObject.transform);//탄 오브젝트 생성
            ob.transform.position = gameObject.transform.position;
            ob.tag = "explosive";
            Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
            ob.transform.SetParent(null, true);
            ob.GetComponent<enemy_bullet>().damage = W_Info[0];
            ob.GetComponent<enemy_bullet>().timer = W_Info[6];//(데미지, 탄수명)탄에 전달
            //Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

            Vector3 dir = Globals.player_pos;
            //pos.z = 0;
            dir = dir - gameObject.transform.position;
            Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-W_Info[2], W_Info[2]));//집탄도,발사각
            dir = v3rotation * dir;//집탄보정
            dir.Normalize();
            m_rigidbody.velocity = dir * W_Info[3];//탄속보정
            yield return new WaitForSeconds(W_Info[4]);//발사간격
            //Debug.Log(i);
        }
        Debug.Log("탄다씀");
        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = false;
        gameObject.transform.parent.parent.GetComponent<Robot>().Robot_Reload = true;
        gameObject.transform.parent.parent.GetComponent<Robot>().reloadtime = W_Info[1];
    }
    public IEnumerator weaponID_5()//대구경 기관단총
    {
        float[] W_Info = new float[7];
        for (int j = 0; j < 7; j++)
            W_Info[j] = Globals.weapon_Info[4, j];

        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = true;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < W_Info[5]; i++)//탄창 수 만큼 반복
        {
            GameObject ob = Instantiate(bullet, gameObject.transform);
            ob.transform.position = gameObject.transform.position;
            Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
            ob.transform.SetParent(null, true);
            ob.GetComponent<enemy_bullet>().damage = W_Info[0];
            ob.GetComponent<enemy_bullet>().timer = W_Info[6];//(데미지, 탄수명)탄에 전달
            //Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

            Vector3 dir = Globals.player_pos;
            //pos.z = 0;
            dir = dir - gameObject.transform.position;
            Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-W_Info[2], W_Info[2]));//집탄도,발사각
            dir = v3rotation * dir;//집탄보정
            dir.Normalize();
            m_rigidbody.velocity = dir * W_Info[3];//탄속보정
            yield return new WaitForSeconds(W_Info[4]);//발사간격
            //Debug.Log(i);
        }
        Debug.Log("탄다씀");
        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = false;
        gameObject.transform.parent.parent.GetComponent<Robot>().Robot_Reload = true;
        gameObject.transform.parent.parent.GetComponent<Robot>().reloadtime = W_Info[1];
    }
    public IEnumerator weaponID_6()//샷건
    {
        float[] W_Info = new float[7];
        for (int j = 0; j < 7; j++)
            W_Info[j] = Globals.weapon_Info[5, j];

        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = true;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < W_Info[5]; i++)//탄창 수 만큼 반복
        {
            GameObject ob = Instantiate(bullet, gameObject.transform);
            ob.transform.position = gameObject.transform.position;
            Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
            ob.transform.SetParent(null, true);
            ob.GetComponent<enemy_bullet>().damage = W_Info[0];
            ob.GetComponent<enemy_bullet>().timer = W_Info[6];//(데미지, 탄수명)탄에 전달
            //Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

            Vector3 dir = Globals.player_pos;
            //pos.z = 0;
            dir = dir - gameObject.transform.position;
            Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-W_Info[2], W_Info[2]));//집탄도,발사각
            dir = v3rotation * dir;//집탄보정
            dir.Normalize();
            m_rigidbody.velocity = dir * W_Info[3];//탄속보정
            //yield return new WaitForSeconds(W_Info[4]);//발사간격
            //Debug.Log(i);
        }
        Debug.Log("탄다씀");
        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = false;
        gameObject.transform.parent.parent.GetComponent<Robot>().Robot_Reload = true;
        gameObject.transform.parent.parent.GetComponent<Robot>().reloadtime = W_Info[1];
    }
    public IEnumerator weaponID_7()//딱총
    {
        float[] W_Info = new float[7];
        for (int j = 0; j < 7; j++)
            W_Info[j] = Globals.weapon_Info[6, j];

        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = true;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < W_Info[5]; i++)//탄창 수 만큼 반복
        {
            GameObject ob = Instantiate(bullet, gameObject.transform);
            ob.transform.position = gameObject.transform.position;
            Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
            ob.transform.SetParent(null, true);
            ob.GetComponent<enemy_bullet>().damage = W_Info[0];
            ob.GetComponent<enemy_bullet>().timer = W_Info[6];//(데미지, 탄수명)탄에 전달
            //Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

            Vector3 dir = Globals.player_pos;
            //pos.z = 0;
            dir = dir - gameObject.transform.position;
            Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-W_Info[2], W_Info[2]));//집탄도,발사각
            dir = v3rotation * dir;//집탄보정
            dir.Normalize();
            m_rigidbody.velocity = dir * W_Info[3];//탄속보정
            yield return new WaitForSeconds(W_Info[4]);//발사간격
            //Debug.Log(i);
        }
        Debug.Log("탄다씀");
        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = false;
        gameObject.transform.parent.parent.GetComponent<Robot>().Robot_Reload = true;
        gameObject.transform.parent.parent.GetComponent<Robot>().reloadtime = W_Info[1];
    }
    public IEnumerator weaponID_8()//방탄방패+기관단총
    {
        float[] W_Info = new float[7];
        for (int j = 0; j < 7; j++)
            W_Info[j] = Globals.weapon_Info[7, j];

        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = true;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < W_Info[5]; i++)//탄창 수 만큼 반복
        {
            GameObject ob = Instantiate(bullet, gameObject.transform);
            ob.transform.position = gameObject.transform.position;
            Rigidbody2D m_rigidbody = ob.GetComponent<Rigidbody2D>();
            ob.transform.SetParent(null, true);
            ob.GetComponent<enemy_bullet>().damage = W_Info[0];
            ob.GetComponent<enemy_bullet>().timer = W_Info[6];//(데미지, 탄수명)탄에 전달
            //Vector3 p_velocity = gameObject.GetComponent<Rigidbody2D>().velocity;

            Vector3 dir = Globals.player_pos;
            //pos.z = 0;
            dir = dir - gameObject.transform.position;
            Quaternion v3rotation = Quaternion.Euler(0f, 0f, Random.Range(-W_Info[2], W_Info[2]));//집탄도,발사각
            dir = v3rotation * dir;//집탄보정
            dir.Normalize();
            m_rigidbody.velocity = dir * W_Info[3];//탄속보정
            yield return new WaitForSeconds(W_Info[4]);//발사간격
            //Debug.Log(i);
        }
        Debug.Log("탄다씀");
        gameObject.transform.parent.parent.GetComponent<Robot>().shoting = false;
        gameObject.transform.parent.parent.GetComponent<Robot>().Robot_Reload = true;
        gameObject.transform.parent.parent.GetComponent<Robot>().reloadtime = W_Info[1];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    private float radius, timer,shotrange;
    public int HP = 500;
    public bool restriction, Robot_Reload, shoting;
    public bool trap;
    public float Rtime = 0;//밧줄속박시간
    public float Ttime = 0;//트랩속박시간
    public float bullet_speed = 200;
    public float reloadtime;

    private Globals.Status myStatus = Globals.Status.Standby;
    private List<Node> route;

    private const float moveSpeed = 24;
    public GameManager Mgr; //GameManager의 메소드를 호출하기 위한 인스턴스
    public GameObject Jack; //플레이어 오브젝트
    public GameObject CardKey;//카드키
    public GameObject bullet;//총알
    public GameObject shotstart;
    public GameObject HP_bar;

    // Start is called before the first frame update
    void Start()
    {
        radius = gameObject.GetComponent<Collider2D>().bounds.size.magnitude / 2;
        if (transform.GetChild(1).tag == "weaponID_1")
            shotrange = Globals.weapon_Info[0, 3] * Globals.weapon_Info[0, 6];
        else if (transform.GetChild(1).tag == "weaponID_2")
            shotrange = Globals.weapon_Info[1, 3] * Globals.weapon_Info[1, 6];
        else if (transform.GetChild(1).tag == "weaponID_3")
            shotrange = Globals.weapon_Info[2, 3] * Globals.weapon_Info[2, 6];
        else if (transform.GetChild(1).tag == "weaponID_4")
            shotrange = Globals.weapon_Info[3, 3] * Globals.weapon_Info[3, 6];
        else if (transform.GetChild(1).tag == "weaponID_5")
            shotrange = Globals.weapon_Info[4, 3] * Globals.weapon_Info[4, 6];
        else if (transform.GetChild(1).tag == "weaponID_6")
            shotrange = Globals.weapon_Info[5, 3] * Globals.weapon_Info[5, 6];
        else if (transform.GetChild(1).tag == "weaponID_7")
            shotrange = Globals.weapon_Info[6, 3] * Globals.weapon_Info[6, 6];
        else if (transform.GetChild(1).tag == "weaponID_8")
            shotrange = Globals.weapon_Info[7, 3] * Globals.weapon_Info[7, 6];
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            int i = 9;//Random.Range(0, 1);
            if (i == 9)
            {
                Instantiate(CardKey, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1),Quaternion.identity);
            }
            Destroy(gameObject);
        }
        //Something();
        if(Globals.status == Globals.Status.Standby)
        {
            if (myStatus != Globals.Status.Standby)
            {
                myStatus = Globals.Status.Standby;
                StopCoroutine("GetRoute");
            }
        }
        else if(Globals.status == Globals.Status.Alert)
        {
            if(myStatus != Globals.Status.Alert)
            {
                myStatus = Globals.Status.Alert;
                StopCoroutine("GetRoute");
            }
            Patrol();
        }else if(Globals.status == Globals.Status.Combat)
        {
            if(myStatus != Globals.Status.Combat)
            {
                myStatus = Globals.Status.Combat;
                StartCoroutine("GetRoute");
            }
        }

        if (restriction == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Rtime += Time.deltaTime;
            if (Rtime >= 4f)
            {
                restriction = false;
                Rtime = 0;
            }
        }
        else if (Rtime > 0)
            Rtime = 0;
        HP_bar.GetComponent<Image>().fillAmount = HP / 500.0f;
    }
    
    void OnTriggerEnter2D(Collider2D collider)  // trap
    {
        if(collider.gameObject.tag == "trap")
        {
            trap = true;
            HP -= 20;
            Destroy(collider.gameObject);
        }
    }
   
    private void Something()
    {
        if (restriction == true)
        {
            //while(restriction == true)
                //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Rtime += Time.deltaTime;
            if (Rtime >= 2.4f)
            {
                gameObject.tag = "Robot";
                restriction = false;
                Rtime = 0;
            }
        }
        else if (Rtime > 0)
            Rtime = 0;
        if (trap == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Ttime += Time.deltaTime;
            if (Ttime >= 3f)
            {
                trap = false;
                Ttime = 0;
            }
        }
        if (trap == false && restriction == false)
            transform.Translate(new Vector2(-3 * Time.deltaTime, 0));
    }


    private IEnumerator GetRoute()
    {
        while (true)
        {
            if(restriction != true)
            {
                StopCoroutine("Trace");
                StartCoroutine("Trace");
            }
            yield return new WaitForSeconds(0.5f);
        }

    }

    private IEnumerator Trace() //GameManager.Pathfinding()을 실행하고 반환받은 경로를 따라 이동하는 함수
    {
        Start:
        //Debug.Log(Jack.transform.position);
        Vector2Int srcPos = Vector2Int.FloorToInt(transform.position);
        Vector2Int dstPos = Vector2Int.FloorToInt(Jack.transform.position);
        route = PathFinder.Pathfinder(srcPos, dstPos, radius);
        
        int index = 0;
        if (route.Count <= 2) yield break;

        Node currentNode;
        Node nextNode;
        while (index < route.Count - 2)
         {
            currentNode = route[index];
            nextNode = route[index + 1];
            if (gameObject.GetComponent<Collider2D>().bounds.Contains(nextNode.position))
            {
                index++;
            }
            Vector2 forceVector = (nextNode.position - currentNode.position).normalized * moveSpeed;
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, shotrange);
            bool playerIN = false;
            foreach (Collider2D hit in collider)
            {
                //Debug.Log("foreachin");
                if (hit.gameObject.tag == "Player")
                {
                    //Debug.Log("player detected");
                    RaycastHit2D hit2 = Physics2D.Raycast(transform.position, (hit.gameObject.transform.position - gameObject.transform.position).normalized, shotrange);
                    //Debug.DrawRay(transform.position, (collider.gameObject.transform.position - gameObject.transform.position).normalized * 20, Color.red, 1.0f);
                    if (hit2 == hit)//범위안에 플레이어가 있고 사격시야안
                    {
                        playerIN = true;
                        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
                        if (Robot_Reload == false && shoting == false)
                        {
                            if (transform.GetChild(1).tag == "weaponID_1")
                            {
                                transform.GetChild(1).GetChild(0).GetComponent<enemy_weapon>().callcoroutine(1);
                                shoting = true;
                            } 
                            else if (transform.GetChild(1).tag == "weaponID_2")
                            {
                                transform.GetChild(1).GetChild(0).GetComponent<enemy_weapon>().callcoroutine(2);
                                shoting = true;
                            }
                            else if (transform.GetChild(1).tag == "weaponID_3")
                            {
                                transform.GetChild(1).GetChild(0).GetComponent<enemy_weapon>().callcoroutine(3);
                                shoting = true;
                            }
                            else if (transform.GetChild(1).tag == "weaponID_4")
                            {
                                transform.GetChild(1).GetChild(0).GetComponent<enemy_weapon>().callcoroutine(4);
                                shoting = true;
                            }
                            else if (transform.GetChild(1).tag == "weaponID_5")
                            {
                                transform.GetChild(1).GetChild(0).GetComponent<enemy_weapon>().callcoroutine(5);
                                shoting = true;
                            }
                            else if (transform.GetChild(1).tag == "weaponID_6")
                            {
                                transform.GetChild(1).GetChild(0).GetComponent<enemy_weapon>().callcoroutine(6);
                                shoting = true;
                            }
                            else if (transform.GetChild(1).tag == "weaponID_7")
                            {
                                transform.GetChild(1).GetChild(0).GetComponent<enemy_weapon>().callcoroutine(7);
                                shoting = true;
                            }
                            else if (transform.GetChild(1).tag == "weaponID_8")
                            {
                                transform.GetChild(1).GetChild(0).GetComponent<enemy_weapon>().callcoroutine(8);
                                shoting = true;
                            }
                            yield return new WaitForFixedUpdate();
                        }
                        else
                        {
                            if(!shoting)
                            {
                                timer += Time.deltaTime;
                                if (timer >= reloadtime)
                                {
                                    Robot_Reload = false;
                                    timer = 0;
                                }
                                if (restriction == false)
                                    if ((Globals.player_pos - transform.position).magnitude > shotrange)
                                        gameObject.GetComponent<Rigidbody2D>().velocity = (Globals.player_pos - transform.position).normalized * moveSpeed;
                            }
                            yield return new WaitForFixedUpdate();
                        }
                    }
                }
            }
            if(playerIN == false)//범위밖 또는 사격시야밖
            {
                if (!shoting)
                {
                    if (restriction == false)
                    {
                        if ((Globals.player_pos - transform.position).magnitude > shotrange)
                            gameObject.GetComponent<Rigidbody2D>().velocity = (Globals.player_pos - transform.position).normalized * moveSpeed;
                        else
                            gameObject.GetComponent<Rigidbody2D>().velocity = forceVector;
                    }
                }
                yield return new WaitForFixedUpdate();
            }
            
         }
         //Debug.Log("Reached");




        /*
        List<Node> nodeList = Mgr.PathFinding(Vector2Int.RoundToInt(gameObject.transform.position), Vector2Int.RoundToInt(Jack.transform.position), 6.4f);
        Node currentNode, nextNode;
        //Debug.Log("Trace on");
        // 경로 따라 이동하기
        // 비동기식으로 만들자
        int index = 0;
        while (nodeList[index] != null)
        {
            currentNode = nodeList[index];
            nextNode = nodeList[index + 1];
            Vector2 v = (nextNode.position - gameObject.transform.position);
            gameObject.GetComponent<Rigidbody2D>().AddForce((v.normalized) * moveSpeed);

            //Debug.Log("Tracing");
            while (!gameObject.GetComponent<Collider2D>().bounds.Contains(nextNode.position)){
                gameObject.GetComponent<Rigidbody2D>().AddForce((v.normalized) * moveSpeed);

                //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
                yield return new WaitForSeconds(0.2f);
            }
            index++;
        }
        //Debug.Log("Trace off");
        */
    }

    IEnumerator Timer(int time)
    {
        yield return new WaitForSeconds(time);
    }

    private void Detect()   // 플레이어를 발견했을떄
    {
        if(Globals.status == Globals.Status.Standby)
        {
            Globals.status = Globals.Status.Combat;
            Alert();
        }
        else
        {

        }
    }

    private void Alert()    // 다른 적들에게 경보
    {
        Globals.player_pos = Globals.player.transform.position;
    }

    private void Patrol()   // 순찰
    {

    }
}

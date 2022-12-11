using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
//using Random = System.Random;

public class map_creating : MonoBehaviour
{

    public GameObject cube;
    public static List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    public Tilemap tilemap,tilemap_ground;
    public TileBase[] tilebase = new TileBase[33];
    public GameObject ground;
    public NavMeshSurface[] surfaces;
    public GameObject Player;

    public GameObject[] way = new GameObject [4];
    public GameObject[] enemy = new GameObject[3];

    private List<Vector3> ground_pos;

    public GameObject drone;

    int enemyNum;

    private void Awake()
    {
        data = CSVReader.Read(GameManager.Instance.map_name);
        Player = GameObject.Find("Player");
        ground_pos = new List<Vector3>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.map_name == "map2")
        {
            Player.transform.position = new Vector3(38, 2, -3);
            Camera.main.transform.position = new Vector3(Player.transform.position.x,10, Player.transform.position.z);
        }
            
        //�ٴ� ũ�� ����
        ground.transform.position = new Vector3(data[0].Count / 2f - 1, 0, -(data.Count / 2f) + 0.5f);
        ground.transform.localScale = new Vector3((data[0].Count / 10f) - 0.1f, 0, data.Count / 10f);
        GameObject a = null;
        int k = 0;
        int h = 0;
        for (int i =0; i<data.Count;i++)
        {
            for(int j =1;j<data[0].Count;j++)
            {
                switch(int.Parse(data[i][Convert.ToString(j)].ToString()))
                {
                    case 0://�ٴ� Ÿ�� ���
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        int waypoint = UnityEngine.Random.Range(0, 101);
                        if (waypoint % 50 == 0 && k<4)
                        {
                            way[k].transform.position = new Vector3(j-1, 2, -i);
                            k++;
                        }
                        ground_pos.Add(new Vector3(j - 1, 1.5f, -i));
                        break;
                    case 1://���� �߾Ӻ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[6]);
                        
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 2://�Ʒ� �߾Ӻ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[9]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 3://���� �߾Ӻ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[12]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 4://������ �߾Ӻ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[15]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;

                    case 5://���� ���ʺ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[5]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 6://�Ʒ��� ���ʺ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[8]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 7://���� ����
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[11]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 8://������ ����
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[14]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;

                    case 9://���� ������
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[7]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 10://�Ʒ��� ������
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[10]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 11://���� �Ʒ���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[13]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 12://������ �Ʒ���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[16]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;

                    case 20://1��и�2���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[1]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 21://2��и�2���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[2]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 22://3��и�2���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[3]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 23://14�и�2���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[4]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 50://�߾� ���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[37]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 60://���¹�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[29]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 61://���칮
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[30]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 62://���¹�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[31]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 63://�Ͽ칮
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[32]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 64://�»�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[33]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 65://���Ϲ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[34]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 66://���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[35]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 67://���Ϲ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[36]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;

                    case 70://����
                        break;

                    case 80://�÷��̾�
                        break;
                    case 81://���
                        break;
                    case 82://�κ�
                        break;

                }
            }
        }
        Instantiate(drone, new Vector3(3f, 2, -1f), Quaternion.Euler(90,0,0));
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].RemoveData();
            surfaces[i].BuildNavMesh();
        }
        if (GameManager.Instance.map_name == "testmap")
            enemyNum = 6;
        else if (GameManager.Instance.map_name == "map2")
            enemyNum = 10;
        for (int i =0;i< UnityEngine.Random.Range(enemyNum-6, 6); i++)
        {
            Instantiate(drone, ground_pos[UnityEngine.Random.Range(0, ground_pos.Count)], Quaternion.Euler(90, 0, 0));
        }
        foreach (Transform child in GameObject.Find("walls").transform)
        {
            child.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }

}

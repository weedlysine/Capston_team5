using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class map_creating : MonoBehaviour
{

    public GameObject cube;
    public static List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    public Tilemap tilemap,tilemap_ground;
    public TileBase[] tilebase = new TileBase[33];
    public GameObject ground;
    public NavMeshSurface[] surfaces; 

    public GameObject[] way = new GameObject [4];

    private void Awake()
    {
        data = CSVReader.Read("testmap");
    }
    // Start is called before the first frame update
    void Start()
    {
        //바닥 크기 설정
        ground.transform.position = new Vector3(data[0].Count / 2f - 1, 0, -(data.Count / 2f) + 0.5f);
        ground.transform.localScale = new Vector3((data[0].Count / 10f) - 0.1f, 0, data.Count / 10f);
        GameObject a = null;
        int k = 0;
        for (int i =0; i<data.Count;i++)
        {
            for(int j =1;j<data[0].Count;j++)
            {
                switch(int.Parse(data[i][Convert.ToString(j)].ToString()))
                {
                    case 0://바닥 타일 깔기
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        int waypoint = UnityEngine.Random.Range(0, 5);
                        if (waypoint % 5 == 0 && k<=3)
                        {
                            Instantiate(way[k], new Vector3(j , 2, -i-1), Quaternion.Euler(new Vector3(90, 0, 0)));
                            k++;
                        }
                        break;
                    case 1://위쪽 중앙벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[6]);
                        
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 2://아래 중앙벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[9]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 3://왼쪽 중앙벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[12]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 4://오른쪽 중앙벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[15]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;

                    case 5://위쪽 왼쪽벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[5]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 6://아래쪽 왼쪽벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[8]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 7://왼쪽 위벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[11]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 8://오른쪽 위벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[14]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;

                    case 9://위쪽 오른벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[7]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 10://아래쪽 오른벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[10]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 11://왼쪽 아래벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[13]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 12://오른쪽 아래벽
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[16]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;

                    case 20://1사분면2기둥
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[1]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 21://2사분면2기둥
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[2]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 22://3사분면2기둥
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[3]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 23://14분면2기둥
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[4]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 50://중앙 기둥
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[32]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 60://세로문
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[29]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 61://가로문
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        a.transform.parent = GameObject.Find("walls").transform;
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[30]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        //a.GetComponent<MeshRenderer>().enabled = false;
                        break;

                    case 70://상자
                        break;

                    case 80://플레이어
                        break;
                    case 81://드론
                        break;
                    case 82://로봇
                        break;

                }
            }
        }

        for (int i = 0; i < surfaces.Length; i++)
            surfaces[i].BuildNavMesh();
        foreach (Transform child in GameObject.Find("walls").transform)
        {
            child.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}

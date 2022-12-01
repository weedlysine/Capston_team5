using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class map_creating : MonoBehaviour
{

    public GameObject cube;
    public static List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    public Tilemap tilemap,tilemap_ground;
    public TileBase[] tilebase = new TileBase[33];
    public GameObject ground;


    private void Awake()
    {
        data = CSVReader.Read("testmap");
    }
    // Start is called before the first frame update
    void Start()
    {
        //�ٴ� ũ�� ����
        ground.transform.position = new Vector3(data[0].Count / 2f - 1, 0, -(data.Count / 2f) + 0.5f);
        ground.transform.localScale = new Vector3((data[0].Count / 10f) - 0.1f, 0, data.Count / 10f);
        GameObject a = null;
        for (int i =0; i<data.Count;i++)
        {
            for(int j =1;j<data[0].Count;j++)
            {
                switch(int.Parse(data[i][Convert.ToString(j)].ToString()))
                {
                    case 0://�ٴ� Ÿ�� ���
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        break;
                    case 1://���� ���κ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[6]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 2://�Ʒ��� ���κ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[9]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 3://���� ���κ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[12]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 4://������ ���κ�
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[15]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 20://1��и�2���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[1]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 21://2��и�2���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[2]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 22://3��и�2���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[3]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 23://14�и�2���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[4]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    case 50://�߾� ���
                        a = Instantiate(cube, new Vector3(j - 1, 1.5f, -i), Quaternion.identity);
                        tilemap.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[32]);
                        tilemap_ground.SetTile(new Vector3Int(j - 1, -i - 1, 0), tilebase[0]);
                        a.GetComponent<MeshRenderer>().enabled = false;
                        break;

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
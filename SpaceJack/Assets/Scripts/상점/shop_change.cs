using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shop_change : MonoBehaviour
{
    public GameObject Ship_image;
    public Sprite[] ship = new Sprite[11];
    int shipNum;
    // Start is called before the first frame update
    void Start()
    {
        ship = Resources.LoadAll<Sprite>("map");
        int i = Random.Range(0, 11);
        shipNum = i;
        Ship_image.GetComponent<Image>().sprite = ship[shipNum];
    }

    public void GameStart()
    {
        SceneManager.LoadScene("map_testing");
    }
    public void Ship_change()
    {
        while(true)
        {
            int i = Random.Range(0, 11);
            if (i != shipNum)
            {
                shipNum = i;
                Debug.Log(shipNum);
                Ship_image.GetComponent<Image>().sprite = ship[shipNum];
                break;
            }
        }
    }
}

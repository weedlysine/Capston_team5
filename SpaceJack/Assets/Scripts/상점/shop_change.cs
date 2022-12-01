using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shop_change : MonoBehaviour
{
    public GameObject SH;
    public GameObject Ship_image;
    public Sprite[] ship = new Sprite[7];
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, 7);
        Ship_image.GetComponent<Image>().sprite = ship[i];
        SH.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void item_shop()
    {
        SH.SetActive(false);
    }

    public void stage_shop()
    {
        SH.SetActive(true);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("map_testing");
    }
    public void Ship_change()
    {
        int i = Random.Range(0, 7);
        Ship_image.GetComponent<Image>().sprite = ship[i];
    }
}

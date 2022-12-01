using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class All_button_Script : MonoBehaviour
{
    public void startscene_start()
    {
        SceneManager.LoadScene("shop");
    }
    public void startscene_option()
    {
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
    }
    public void startscene_exit()
    {
        Application.Quit();
    }
}

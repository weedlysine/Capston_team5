using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public void bLoad()
    {
        DataManager.Instance.LoadGameData();
    }
}

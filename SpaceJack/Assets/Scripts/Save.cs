using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public void bsave()
    {
        DataManager.Instance.SaveGameData();
    }
}

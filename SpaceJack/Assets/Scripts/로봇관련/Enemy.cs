using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected static bool status = false;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 100;i>1;i--)
        {
            Debug.Log(i + "asdasdasd");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void hit()
    {
        // 공격에 맞았을 때 함수
    }

    public virtual void detect(){
        // 플레이어를 발견했을 때 함수
    }

    public virtual void patrol(){
        // 돌아다니게 하는 함수
    }
}

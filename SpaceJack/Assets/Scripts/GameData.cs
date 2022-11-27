using System;
using UnityEngine;

[Serializable] // 직렬화

public class Data
{
    public GameObject bullet;
    public GameObject bomb;
    public GameObject rope;
    public GameObject HP_bar;
    public GameObject Q;
    public GameObject W;
    public GameObject E;
    public GameObject R;
    public GameObject CK;//카드키(cardkey)

    GameObject[] robots;
    public float bullet_speed;
    public float speed;
    public GameObject LB6, LB5, LB4, LB3, LB2, LB1, CT;
    public GameObject rope_target, shotstart;
    public float Jack_HP;
    public int armor_HP , damage;
    public bool rope_restriction;
}
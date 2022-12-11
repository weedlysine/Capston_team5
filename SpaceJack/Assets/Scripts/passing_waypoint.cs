using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class passing_waypoint : MonoBehaviour
{
    BehaviorTree behaviorTree;
    SharedGameObjectList asd;

    private GameObject Player;
    private List<GameObject> waypointList;
    [SerializeField]
    private List<GameObject> temp;
    int templength;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        behaviorTree = GetComponent<BehaviorTree>();
        waypointList = new List<GameObject>();
        asd = new SharedGameObjectList();
        GameObject waypointgroup = GameObject.Find("way point");
        for (int i=0;i<waypointgroup.transform.childCount;i++)
        {
            temp.Add(waypointgroup.transform.GetChild(i).gameObject);
        }
        templength = temp.Count;
        for (int i =0; i< Random.Range(2, templength+1); i++)
        {
            int num = Random.Range(0, temp.Count);
            waypointList.Add(temp[num]);
            temp.RemoveAt(num);
        }
        asd.SetValue(waypointList);
        behaviorTree.SetVariableValue("waypoint list", asd);
        behaviorTree.SetVariableValue("player", Player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

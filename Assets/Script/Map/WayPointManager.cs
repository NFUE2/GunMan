using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    GameObject EnemyList;

    // Start is called before the first frame update
    void Start()
    {
        EnemyList = GameObject.Find("EnemyList");
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform[] WayPointList = new Transform[transform.GetChild(i).childCount];

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
                WayPointList.SetValue(transform.GetChild(i).GetChild(j).GetComponent<Transform>(), j);

            EnemyList.transform.GetChild(i).GetComponent<EnemyMove>().Set_WayPoint = WayPointList;
        }
    }
}

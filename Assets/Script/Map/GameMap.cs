using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMap : MonoBehaviour
{
    [SerializeField]
    Transform StartPos;

    [SerializeField]
    GameObject[] Enemy;

    [SerializeField]
    Transform[] SpawnPoint;

    private static GameMap Instance;
    public static GameMap GetInstance { get { return Instance; } }

    private void OnEnable()
    {
        Instance = this;
        for (int i = 0; i  < SpawnPoint.Length; i ++)
        {
            GameObject Obj;
            if (Enemy.Length == 1)
                Obj = Instantiate(Enemy[0]);
            else
                Obj = Instantiate(Enemy[i]);

            if(GameObject.Find("EnemyList") == null)
            {
                GameObject list = new GameObject("EnemyList");
                list.tag = "Enemy";
                //list.transform.SetParent(transform);
            }


            Obj.transform.SetParent(GameObject.Find("EnemyList").transform);
            Obj.transform.position = SpawnPoint[i].transform.position;
            Obj.tag = "Enemy";
            Obj.layer = 9;
            ObjectManager.GetInstance.SetGameObject(Obj);
        }

        GameObject Player = GameObject.Find("Player");

        Player.transform.position = StartPos.position;
        
    }

    
}
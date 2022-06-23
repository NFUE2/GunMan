using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    Material Open_Material;

    private static StageManager Instance = null;

    public static StageManager GetInstance{ get { return Instance; } }

    private void Awake()
    {
        Instance = this;
    }


    private void OnEnable()
    {
        StageCheck();
    }

    //스테이지를 돌면서 클리어했을때 스테이지를 열어줄것
    void StageCheck()
    {
        int chk = GameManager.GetInstance.StageClear;

        bool AllClear = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if(AllClear)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                AllClear = false;
            }
            if (!transform.GetChild(i).gameObject.activeSelf) break;


            for (int j = 0; j <= chk; j++)
            {
                if(j == transform.GetChild(i).childCount)
                {
                    chk -= transform.GetChild(i).childCount;
                    AllClear = true;
                    break;
                }
                transform.GetChild(i).GetChild(j).GetComponent<StageState>().GS_Open = true;
            }
        }
    }
}
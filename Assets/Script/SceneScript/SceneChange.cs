using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneChange : MonoBehaviour
{
    string[] Scene = { "WorldMap", "InGame" };

    public void RoadWorldMap()
    {
        if(ObjectManager.GetInstance.EnemyCount == 0)
            GameManager.GetInstance.StageClear++;
        SceneController.SetScene(Scene[0]);
    }

    public void RoadInGame()
    {
        SceneController.SetScene(Scene[1]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Create_InGameMap : EditorWindow
{
    static List<GameObject> OpenNode;
    static List<GameObject> CloseNode;

    Object[] BoxTile;

    [MenuItem("MapEditor/InGame")]
    static void showWindow()
    {
        GetWindow(typeof(Create_InGameMap)).Show();
        NewStage();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("New Map", GUILayout.MaxHeight(20), GUILayout.MaxWidth(100)))
        {
            OpenNode.Clear();
            CloseNode.Clear();
            NewStage();
        }
        //BoxTile = Resources.LoadAll<Object>("Tile/InGame");

        //int select = GUILayout.SelectionGrid();
    }

    void ViewTile()
    {




    }

    static void NewStage()
    {
        //전에 만들었던 맵툴이 껐다켤때마다 새로운 개체로 나오던걸 수정하고싶었음
        if (GameObject.Find("OpenNode") == null)
            new GameObject("OpenNode");

        else
        {

            GameObject ON = GameObject.Find("OpenNode");
            for (int i = 0; i < ON.transform.childCount; i++)
                OpenNode.Add(ON.transform.GetChild(i).gameObject);
        }

        if (GameObject.Find("CloseNode") == null)
            new GameObject("CloseNode");

        else
        {
            GameObject ON = GameObject.Find("CloseNode");
            for (int i = 0; i < ON.transform.childCount; i++)
                OpenNode.Add(ON.transform.GetChild(i).gameObject);
        }
    }
}

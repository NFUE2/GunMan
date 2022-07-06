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
        OpenNode = new List<GameObject>();
        CloseNode = new List<GameObject>();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Map", GUILayout.MaxHeight(20), GUILayout.MaxWidth(100)))
            NewStage();

        if (GUILayout.Button("Clear", GUILayout.MaxHeight(20), GUILayout.MaxWidth(100)))
            delete();
        GUILayout.EndHorizontal();

        //BoxTile = Resources.LoadAll<Object>("Tile/InGame");

        //int select = GUILayout.SelectionGrid();
    }

    void ViewTile()
    {




    }

    
    static void NewStage()
    {
        if (OpenNode == null)
        {
            OpenNode = new List<GameObject>();
            CloseNode = new List<GameObject>();
        }
        else
            Debug.Log("새로운 노드가 존재합니다.");

        if (OpenNode.Count != 0)
            OpenNode.Clear();

        if (CloseNode.Count != 0)
            CloseNode.Clear();
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

    //노드 삭제함수
    void delete()
    {
        if(GameObject.Find("OpenNode") == null)
        {
            Debug.Log("삭제할 노드가 존재하지않습니다.");
            return;
        }
            
        Debug.Log("삭제");


        Destroy(GameObject.Find("OpenNode") as Object);
        Destroy(GameObject.Find("CloseNode") as Object);

        //Destroy함수는 런타임때에 작동해서 에디터 모드에서는 DestroyImmediate함수를 사용해야한다.
        //DestroyImmediate(GameObject.Find("OpenNode") as Object,false);
        //DestroyImmediate(GameObject.Find("CloseNode") as Object,false);

        OpenNode.Clear();
        CloseNode.Clear();
    }
}

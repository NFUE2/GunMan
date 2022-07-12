using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Create_InGameMap : EditorWindow
{
    List<GameObject> OpenNode;
    List<GameObject> CloseNode;

    Object[] BoxTile;

    Vector2 Scroll;
    int ChoiceTileNum;

    [MenuItem("MapEditor/InGame")]
    static void showWindow()
    {
        GetWindow(typeof(Create_InGameMap)).Show();
    }
    private void OnEnable()
    {
        OpenNode = new List<GameObject>();
        CloseNode = new List<GameObject>();
        BoxTile = Resources.LoadAll("Tile/InGame");
        NewStage();
    }

    private void OnDisable()
    {
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Map", GUILayout.MaxHeight(20), GUILayout.MaxWidth(100)))
            NewStage();

        if (GUILayout.Button("Clear", GUILayout.MaxHeight(20), GUILayout.MaxWidth(100)))
            delete();
        GUILayout.EndHorizontal();

        ViewTile();



        //BoxTile = Resources.LoadAll<Object>("Tile/InGame");

        //int select = GUILayout.SelectionGrid();
    }

    //에디터로 사용할 타일들을 미리보기해주는 함수이다.
    //이전에 만든 에디터와 비슷하게 만들었는데 미리보기의 크기가 다들 제각각이라 좀 더 봐야할듯
    void ViewTile()
    {
        Texture[] Thumnail = new Texture[BoxTile.Length];

        Scroll = EditorGUILayout.BeginScrollView(Scroll, GUILayout.MaxHeight(2000.0f), GUILayout.MaxWidth(2000.0f));

        for (int i = 0; i < BoxTile.Length; i++)
            Thumnail[i] = (AssetPreview.GetAssetPreview(BoxTile[i]));

        ChoiceTileNum = GUILayout.SelectionGrid(ChoiceTileNum,Thumnail, 4, GUILayout.MaxHeight(500.0f), GUILayout.MaxWidth(500.0f));

        EditorGUILayout.EndScrollView();
    }

    void NewStage()
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

        //Destroy(GameObject.Find("OpenNode") as Object);
        //Destroy(GameObject.Find("CloseNode") as Object);

        //destroy함수는 런타임때에 작동해서 에디터 모드에서는 destroyimmediate함수를 사용해야한다.
        DestroyImmediate(GameObject.Find("OpenNode") as Object, false);
        DestroyImmediate(GameObject.Find("CloseNode") as Object, false);

        OpenNode.Clear();
        CloseNode.Clear();
    }

}

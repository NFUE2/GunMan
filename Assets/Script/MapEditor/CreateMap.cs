using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateMap : EditorWindow
{
    private static CreateMap Instance = null;
    public static CreateMap GetInstance { get { return Instance; } }


    private void OnHierarchyChange()
    {
        Instance = this;
    }

    //노드로 받을 2종류 
    public List<Vector3> OpenNode = new List<Vector3>();
    public List<Vector3> CloseNode = new List<Vector3>();

    bool SetTile = false; //토글용 bool값

    //유니티에서 보일 탭 창
    [MenuItem("MapEditor/Create_Map")]
    static void ShowWindow()
    {
        GetWindow(typeof(CreateMap)).Show(); //탭창 보이게하기
    }

    string[] Tiles = { "BaseTile", "RoadTile", "WaterTile", "DecoTile" }; //타일을 넣을 종류
    //string[] Material = { "Green", "White" };
    string[] TilesRotation = { "0", "60", "120", "180", "240", "300" };


    int TileType; //선택할것
    //int MaterialType;
    int TileRotate;

    //내맘대로 넣을 타일종류들
    private Object[] BaseTile = null;
    private Object[] RoadTile = null;
    private Object[] WaterTile = null;
    private Object[] DecoTile = null;

    //타일의 색을 변경할 메테리얼들
    //private Object[] Materials;

    int choice;
    Vector2 Scroll;
    public GameObject ChoiceTile;

    //tap창 내에서 보이게할 것들
    private void OnGUI()
    {
        //Resources안쓰고 받아오기
        //SetTile = EditorGUILayout.Toggle("Set Tile", SetTile); //토글 생성
        //if (SetTile)
        //{
        //    SerializedObject Obj = new SerializedObject(this); //
        //    EditorGUILayout.PropertyField(Obj.FindProperty("BaseTile"));
        //    EditorGUILayout.PropertyField(Obj.FindProperty("RoadTile"));
        //    EditorGUILayout.PropertyField(Obj.FindProperty("WaterTile"));
        //    EditorGUILayout.PropertyField(Obj.FindProperty("DecoTile"));
        //    Obj.ApplyModifiedProperties();
        //}

        TileType = GUILayout.Toolbar(TileType, Tiles);
        //MaterialType = GUILayout.Toolbar(MaterialType, Material);
        TileRotate = GUILayout.Toolbar(TileRotate, TilesRotation);

        BaseTile = Resources.LoadAll<Object>("Tile/Base");
        RoadTile = Resources.LoadAll<Object>("Tile/Road");
        WaterTile = Resources.LoadAll<Object>("Tile/Water");
        DecoTile = Resources.LoadAll<Object>("Tile/Deco");

        //Materials = Resources.LoadAll<Object>("Tile/Material");

        switch (TileType)
        {
            case 0:
                ViewTile(BaseTile);
                break;
            case 1:
                ViewTile(RoadTile);
                break;
            case 2:
                ViewTile(WaterTile);
                break;
            case 3:
                ViewTile(DecoTile);
                break;
        }

        if (CloseNode.Count == 0)
        {
            //ChoiceTile = RoadTile[10] as GameObject;
            //Create(Vector3.zero);
            Search(Vector3.zero,0.0f,0.0f);
        }
    }


    void ViewTile(Object[] Tile)
    {
        Texture[] Thumnail = new Texture[Tile.Length];

        Scroll = EditorGUILayout.BeginScrollView(Scroll, GUILayout.MaxHeight(((Tile.Length / 4) + 1) * 500.0f), GUILayout.MaxWidth(2000.0f));

        if (Tile.Length > 0)
        {
            for (int i = 0; i < Tile.Length; i++)
                Thumnail[i] = AssetPreview.GetAssetPreview(Tile[i]);

            choice = GUILayout.SelectionGrid(choice, Thumnail, 4, GUILayout.MaxHeight(500.0f), GUILayout.MaxWidth(500.0f)); //그리드로 버튼을 생성함
            ChoiceTile = (GameObject)Tile[choice];
        }
    }

    public void Create(Vector3 Pos) //제작
    {
        if (GameObject.Find("Stage") == null) new GameObject("Stage");

        GameObject Obj = Instantiate(ChoiceTile);
        Obj.transform.position = Pos;
        Obj.transform.rotation = Quaternion.AngleAxis(TileRotate * 60,Vector3.up);
        Obj.AddComponent<MeshCollider>();
        Obj.GetComponent<MeshCollider>().convex = true;

        //Obj.GetComponent<MeshRenderer>().material = Materials[MaterialType] as Material;

        Obj.transform.SetParent(GameObject.Find("Stage").transform);
        Obj.name = "Hex Tile" + GameObject.Find("Stage").transform.childCount;


        OpenNode.Remove(Obj.transform.position); //오픈노드에서 제거
        CloseNode.Add(Obj.transform.position); //클로즈노드에 추가

        Search(Obj.transform.position, 0.0f, 5.0f);
        Search(Obj.transform.position, 0.0f, -5.0f);
        Search(Obj.transform.position, 4.3f, -2.5f);
        Search(Obj.transform.position, 4.3f, 2.5f);
        Search(Obj.transform.position, -4.3f, -2.5f);
        Search(Obj.transform.position, -4.3f, 2.5f);
        NodeCheck();
    }

    void Search(Vector3 Origin, float x, float z) //탐색
    {
        if (GameObject.Find("OpenNode") == null) new GameObject("OpenNode");

        Vector3 SetVec = Origin + new Vector3(x, 0.0f, z);

        if (!OpenNode.Contains(SetVec) && !CloseNode.Contains(SetVec))
        {
            //CloseNode.Add(SetVec);
            OpenNode.Add(SetVec);

            GameObject Obj = Instantiate(Resources.Load<GameObject>("Sphere"));
            Obj.name = "Point";
            Obj.transform.position = SetVec;
            Obj.AddComponent<ViewCreatePosition>();

            Obj.transform.SetParent(GameObject.Find("OpenNode").transform);
        }
    }

    void NodeCheck()
    {
        for (int i = 0; i < CloseNode.Count; i++)
        {
            if(Physics.OverlapSphere(CloseNode[i],1.0f).Length == 0)
            {
                OpenNode.Add(CloseNode[i]);

                GameObject Obj = Instantiate(Resources.Load<GameObject>("Sphere"));
                Obj.name = "Point";
                Obj.transform.position = CloseNode[i];
                Obj.AddComponent<ViewCreatePosition>();

                Obj.transform.SetParent(GameObject.Find("OpenNode").transform);

                CloseNode.Remove(CloseNode[i]);
            }
        }
    }
}
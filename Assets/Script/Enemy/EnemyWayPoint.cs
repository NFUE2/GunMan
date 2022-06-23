using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//적들이 움직이는 방식을 만들것.
//왕복과 편도움직임 방식을 만들것
public class EnemyWayPoint : MonoBehaviour
{
    private static EnemyWayPoint Instance;
    public static EnemyWayPoint GetInstance { get { return Instance; } }

    private void OnEnable()
    {
        Instance = this;
    }


    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        ////도착위치 그리기
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    Gizmos.DrawSphere(transform.GetChild(i).transform.position, 0.5f);
        //    Gizmos.DrawLine(transform.GetChild(i % transform.childCount).transform.position,transform.GetChild((i + 1) % transform.childCount).transform.position);
        //}
}


    //public void Create()
    //{
    //    GameObject Obj = new GameObject("Point" + (transform.childCount + 1));
    //    Obj.transform.position = transform.position;
    //    Obj.transform.SetParent(transform);
    //}
}

//[CustomEditor(typeof(EnemyWayPoint))]
//public class CreateWayPoint : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        if(GUILayout.Button("Create",GUILayout.MaxHeight(50),GUILayout.MaxWidth(50)))
//        {
//            EnemyWayPoint.GetInstance.Create();
//        }
//    }
//}
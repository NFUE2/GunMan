using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    public static void DrawCircle(Vector3 Origin, float radius) 
    {
        Gizmos.color = Color.red;

        //Vector3 forward를 기준잡을것.
        //매개변수로 받은 위치에서 radius만큼 떨어진 위치가 원의 반지름 위치.
        Vector3 Pos1 = Origin + (new Vector3(0, 0, 1) * 10.0f);
        Vector3 Pos2;

        for (int i = 1; i <= 36; i++)
        {
            float seta = (i * Mathf.Deg2Rad) * radius;
            Pos2 =  Origin + new Vector3(Mathf.Sin(seta),0.0f,Mathf.Cos(seta)) * radius;
            Gizmos.DrawLine(Pos1,Pos2);

            Pos1 = Pos2;
        }
    }
}

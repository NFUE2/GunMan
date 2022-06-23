using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ViewCreatePosition : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Handles.DrawWireArc(transform.position,transform.up,transform.right,360f,2.3f);
    }
}

[CustomEditor(typeof(ViewCreatePosition))]
public class Wire : Editor
{
    private void OnSceneGUI()
    {
        ViewCreatePosition Button = (ViewCreatePosition)target;

        if (Handles.Button(Button.transform.position, Quaternion.AngleAxis(90, Button.transform.right), 1.0f, 2.0f, Handles.RectangleHandleCap))
        {
            CreateMap.GetInstance.Create(Button.transform.position);
            Destroy(Button.gameObject);
        }
    }
}


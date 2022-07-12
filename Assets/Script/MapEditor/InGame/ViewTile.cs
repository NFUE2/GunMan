using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ViewTile : MonoBehaviour
{
}

[CustomEditor(typeof(ViewTile))]
public class TileButton : Editor
{

    public void OnSceneGUI()
    {
        ViewTile Button = (ViewTile)target;

        if (Handles.Button(Button.transform.position, Quaternion.AngleAxis(90, Button.transform.right), 1.0f, 2.0f, Handles.RectangleHandleCap))
        {
            CreateMap.GetInstance.Create(Button.transform.position);
            Destroy(Button.gameObject);
        }
        Tools.current = Tool.View;
    }
}

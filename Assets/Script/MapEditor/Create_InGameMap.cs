using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Create_InGameMap : EditorWindow
{
    Object[] BoxTile;

    [MenuItem("MapEditor/InGame")]
    static void showWindow()
    {
        GetWindow(typeof(Create_InGameMap)).Show();
    }

    private void OnGUI()
    {
        BoxTile = Resources.LoadAll<Object>("Tile/InGame");
    }
}

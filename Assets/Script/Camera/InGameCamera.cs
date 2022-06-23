using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    Vector3 Origin;
    Vector3 Target;

    Transform Player;

    Vector3 RockOn;

    // Start is called before the first frame update
    void Start()
    {
        Origin = transform.position;
        Player = GameObject.Find("Player").transform;
        transform.position += Player.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    UnityEditor.EditorApplication.isPlaying = false;

        //Cursor.lockState = CursorLockMode.Confined;
        Ray ScreenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        foreach (RaycastHit Hit in Physics.RaycastAll(ScreenRay, 50.0f))
        {
            if (Hit.collider.gameObject.layer == 8)
            {
                Target = Hit.point;
            }
            else continue;
        }

        RockOn = (Target - Player.position) / 4;
        transform.position = Player.position + Origin + new Vector3(RockOn.x,0.0f,RockOn.z);/** Vector3.Distance(Player.position,Target.position)*/;
    }
}

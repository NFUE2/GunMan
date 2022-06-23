using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageState : MonoBehaviour
{
    [SerializeField]
    Material OpenMaterial;
    public GameObject IngameDate;

    private void Start()
    {
        if (GS_Open)
            this.GetComponent<MeshRenderer>().material = OpenMaterial;
    }

    public bool Open = false;

    public bool GS_Open { get { return Open; } set { Open = value; } }
}

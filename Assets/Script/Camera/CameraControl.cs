using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    Transform player;

    void Start()
    {
        this.transform.position += player.position;
    }

    void Update()
    {
        this.transform.position = (player.position + new Vector3(0,15,-8));
    }
}

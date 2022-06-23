using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCamera : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    Vector3 Origin;

    // Start is called before the first frame update
    void Start()
    {
        Origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position + Origin;
    }
}

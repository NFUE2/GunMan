using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBang : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Flash()
    {
        foreach(Collider col in Physics.OverlapSphere(transform.position,5.0f))
        {
            if(col.tag == "Enemy" && Vector3.Angle(col.transform.forward,transform.position) <=45.0f)
            {
                col.GetComponent<EnemyStat>().Set_Stun = true;
            }
        }
    }
}

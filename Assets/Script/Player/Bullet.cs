using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(1.0f, 100.0f)]
    float speed;

    [SerializeField, Range(1.0f, 100.0f)]
    float Damage;

    Vector3 Origin;
    Vector3 Direction;

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction.normalized * speed * Time.deltaTime;

        if (Vector3.Distance(Origin, transform.position) > 30.0f)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if(other.GetComponent<EnemyStat>() != null)
                other.GetComponent<EnemyStat>().Set_HP = Damage;
            else
                other.GetComponent<BossStat>().Set_Hp = Damage;
            this.gameObject.SetActive(false);
        }
        //else if(other.tag == "Boss")
        //{
        //    this.gameObject.SetActive(false);
        //}
    }

    public Vector3 Set_Direction { set { Direction = value; } }
    public Vector3 Set_Origin { set { Origin = value; } }
}

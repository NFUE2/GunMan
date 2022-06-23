using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public enum State
    {
        Idle,
        Battle,
        Stun,
        Dead,
    }

    [SerializeField,Range(0f, 100.0f)]
    float HealthPoint;

    State EnemyState;
    bool stun;
    void Start()
    {
        EnemyState = State.Idle;
        stun = false;
    }

    void Update()
    {
        if(HealthPoint < 0)
        {
            EnemyState = State.Dead;
            StartCoroutine(Dead());
        }
        if(stun)
        {
            stun = false;
            EnemyState = State.Stun;
            StartCoroutine(Stun());
        }
    }

    IEnumerator Dead()
    {
        ObjectManager.GetInstance.DeleteGameObject(gameObject);
        transform.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
    }

    IEnumerator Stun()
    {
        yield return new WaitForSeconds(2.0f);
        EnemyState = State.Battle;
    }


    public State Get_State { get { return EnemyState; } }
    public float Set_HP { set { HealthPoint -= value; } }
    public bool Set_Stun { set { stun = value; } }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //무기는 알아서 프리팹으로 설정해줄것

    public enum Type
    {
        ShortRange,
        LongRange,
    }

    public Type EnemyType;

    //좌우 손 위치
    [SerializeField] Transform LeftHand = null;
    [SerializeField] Transform RightHand = null;

    [SerializeField, Range(0.0f,50.0f)]
    float Delay;

    [SerializeField, Range(0.0f, 50.0f)]
    float Damage;

    float attack;

    Transform Target =null;

    Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        attack = Delay;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<EnemyStat>().Get_State == EnemyStat.State.Dead || transform.GetComponent<EnemyStat>().Get_State == EnemyStat.State.Stun)
            return;

        Target = transform.GetComponent<EnemyMove>().GetPlayer;

        if(Target != null && Vector3.Distance(transform.position,Target.position) < 2.5f)
        {
            ani.SetTrigger("Attack");
        }

        //if (attack >= Delay && Target != null)
        //{
        //    if(EnemyType == Type.ShortRange && Vector3.Distance(transform.position,Target.position) < 2.0f)
        //    {
        //        Debug.Log("근접 공격");
        //    }
        //    else if(EnemyType == Type.LongRange)
        //    {
        //        if(Vector3.Distance(transform.position,Target.position) < 5.0f)
        //        {
        //            ani.SetBool("Close", true);
        //            Debug.Log("무기 교체");
        //        }
        //        else
        //        {
        //            ani.SetBool("Close", false);
        //            ani.SetTrigger("Shooting");
        //            Debug.Log("원거리 공격");
        //        }
        //    }
        //    attack = 0.0f;
        //}
        //else
        //    attack += Time.deltaTime;
    }

    public void Hit()
    {
        foreach(Collider col in Physics.OverlapSphere(transform.position,5.0f))
        {
            if(col.gameObject.name == "Player")
            {
                ani.ResetTrigger("Attack");
                col.gameObject.GetComponent<PlayerStat>().damage = 1;
            }
        }
    }

    public Type GetState { get { return EnemyType;} }
}




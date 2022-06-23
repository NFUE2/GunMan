using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    EnemyAttack.Type MoveType;

    Transform Player;
    NavMeshAgent agent;
    Animator ani;

    [SerializeField, Range(0.0f, 20.0f)]
    float speed;

    //탐지범위
    [SerializeField,Range(0.0f, 30.0f)]
    float SearchRange;

    [SerializeField, Range(0.0f, 120.0f)]
    float SearchAngle;

    public Transform[] WayPoint;
    int WayPointNumber;

    [SerializeField]
    bool Patrol;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Player = null;
        agent = GetComponent<NavMeshAgent>();
        MoveType = transform.GetComponent<EnemyAttack>().GetState;
    }

    void Start()
    {
        ani = GetComponent<Animator>();
        WayPointNumber = 0;
        //agent.destination = WayPoint[WayPointNumber].position;
        agent.speed = 3.0f;
    }
    
    public Transform[] Set_WayPoint { set { WayPoint = value; } }

    // Update is called once per frame
    void Update()
    {
        //사망시
        if (transform.GetComponent<EnemyStat>().Get_State == EnemyStat.State.Dead)
        {
            ani.SetFloat("MoveSpeed",0.0f);
            ani.SetBool("Dead", true);
            agent.Stop();
            return;
        }
        else
        {
            //플레이어를 찾았을때
            if (Player != null)
            {
                agent.destination = Player.position;
                agent.speed = speed;
                //if (MoveType == EnemyAttack.Type.ShortRange)
                //{
                //    agent.destination = Player.position;
                //    agent.stoppingDistance = 2.0f;
                //}
                //else if (MoveType == EnemyAttack.Type.LongRange)
                //    if (Vector3.Distance(transform.position, Player.position) < 5.0f)
                //    {
                //        agent.destination = Player.position;
                //        agent.stoppingDistance = 2.0f;
                //    }
            }

            else
            {
                if (Vector3.Distance(transform.position, agent.destination) < 0.5f)
                {
                    if (!Patrol)
                    {
                        WayPointNumber = WayPointNumber < WayPoint.Length - 1 ? WayPointNumber + 1 : WayPointNumber;
                        ani.SetFloat("MoveSpeed", 0.0f);
                    }

                    else
                    {
                        WayPointNumber++;
                        WayPointNumber %= WayPoint.Length;
                        ani.SetFloat("MoveSpeed", 0.5f);
                    }
                    agent.destination = WayPoint[WayPointNumber].position;
                }

                Search();
            }
        }

        if(Vector3.Distance(transform.position,agent.destination) > 0.5f)
            ani.SetFloat("MoveSpeed", 1.0f);
        else
            ani.SetFloat("MoveSpeed", 0.0f);
    }

    private void Search()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, SearchRange))
        {
            if (col.gameObject.name == "Player")
            {
                Vector3 dir = col.transform.position - transform.position;
                
                if (Vector3.Angle(transform.forward, dir) < SearchAngle)
                {
                    Player = col.gameObject.transform;
                    agent.destination = Player.position;
                    agent.stoppingDistance = 2.0f;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player").transform;
            agent.destination = Player.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        DrawGizmo.DrawCircle(transform.position,SearchRange);
        //Gizmos.DrawWireSphere(transform.position, SearchRange);

        Gizmos.DrawLine(transform.position, transform.position + DrawLine(SearchAngle) * SearchRange);
        Gizmos.DrawLine(transform.position, transform.position + DrawLine(-SearchAngle) * SearchRange);
    }

    Vector3 DrawLine(float angle)
    {
        angle += transform.rotation.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public Transform GetPlayer { get { return Player; } }
}
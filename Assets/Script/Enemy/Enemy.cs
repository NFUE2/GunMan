using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type
    {
        ShortRange,
        LongRange,
    }

    public Type EnemyType;

    [SerializeField] Transform LeftHand = null;
    [SerializeField] GameObject RightHand = null;

    [SerializeField] GameObject SubWeapon = null;

    Transform Player = null;
    NavMeshAgent agent;

    //탐지범위
    [Range(0.0f, 30.0f)]
    float SearchRange;

    //공격사거리
    [Range(0.0f, 30.0f)]
    float Distance;

    [Range(5.0f, 50.0f)]
    float Attackdealy;

    float Attack;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SearchRange = 0.0f;
        Attack = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyType == Type.ShortRange)
        {
            if(Player != null)
            {
                agent.destination = Player.position;
                agent.stoppingDistance = 2.0f;
            }
        }
        else if(EnemyType == Type.LongRange)
        {

        }

        Attack += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(Player == null) Player = GameObject.Find("Player").transform;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10.0f);
    }
}

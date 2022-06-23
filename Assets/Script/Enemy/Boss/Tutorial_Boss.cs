using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Tutorial_Boss : MonoBehaviour
{
    [SerializeField]
    GameObject Effect;

    [SerializeField]
    GameObject[] Painting_Mesh;

    GameObject Player;
    NavMeshAgent agent;

    Flash_Arc_Pattern Flash_Arc;
    Fill_Arc_Pattern Fill_Arc;

    Flash_Box_Pattern Flash_B;
    Fill_Box_Pattern Fill_B;

    Flash_Circle_Pattern Flash_C;

    Animator ani;

    int Pattern_num;
    bool Attack;

    float BossAttack;

    [SerializeField, Range(1.0f, 20.0f)]
    float[] AttackTime;

    [SerializeField]
    bool[] UsePattern;

    //[SerializeField]
    //bool[] attacking;

    float Delay;

    void Start()
    {
        
        Attack = false;
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        BossAttack = 0.0f;

        Painting_Mesh[0] = GameObject.Find("BossStage(Clone)").transform.Find("Flash_Arc").gameObject;
        Painting_Mesh[1] = GameObject.Find("BossStage(Clone)").transform.Find("Fill_Arc").gameObject;
        Painting_Mesh[2] = GameObject.Find("BossStage(Clone)").transform.Find("Flash_Box").gameObject;
        Painting_Mesh[3] = GameObject.Find("BossStage(Clone)").transform.Find("Fill_Box").gameObject;
        Painting_Mesh[4] = GameObject.Find("BossStage(Clone)").transform.Find("Flash_Circle").gameObject;

        Flash_Arc = Painting_Mesh[0].GetComponent<Flash_Arc_Pattern>();
        Fill_Arc = Painting_Mesh[1].GetComponent<Fill_Arc_Pattern>();
        Flash_B = Painting_Mesh[2].GetComponent<Flash_Box_Pattern>();
        Fill_B = Painting_Mesh[3].GetComponent<Fill_Box_Pattern>();
        Flash_C = Painting_Mesh[4].GetComponent<Flash_Circle_Pattern>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<BossStat>().Get_State == BossStat.State.Dead)
        {
            ObjectManager.GetInstance.DeleteGameObject(gameObject);
            transform.GetComponent<CapsuleCollider>().enabled = false;
            ani.SetFloat("MoveSpeed", 0.0f);
            ani.SetBool("Dead", true);
            agent.enabled = false;
            return;
        }

        Player = GameObject.Find("Player");

        if (!Attack)
        {
            transform.LookAt(Player.transform.position);
            agent.destination = Player.transform.position;
            ani.SetFloat("MoveSpeed", Mathf.Lerp(ani.GetFloat("MoveSpeed"), 1.0f, Time.deltaTime));
            BossAttack += Time.deltaTime;

            if ((Vector3.Distance(Player.transform.position, transform.position) < (Flash_Arc.radius / 2)
                && Vector3.Angle(Player.transform.position, transform.forward) < Flash_Arc.Arc_angle / 2) && (BossAttack > AttackTime[0] && UsePattern[0]))
                Pattern(1);

            else if ((Vector3.Distance(Player.transform.position, transform.position) < (Fill_Arc.radius / 2)
                && Vector3.Angle(Player.transform.position, transform.forward) < Fill_Arc.Arc_angle / 2) && (BossAttack > AttackTime[1]) && UsePattern[1])
                Pattern(2);

            else if (Vector3.Distance(Player.transform.position, transform.position) < Flash_B.length && (BossAttack > AttackTime[2]) && UsePattern[2])
                Pattern(3);

            else if (Vector3.Distance(Player.transform.position, transform.position) < Fill_B.length && (BossAttack > AttackTime[3]) && UsePattern[3])
                Pattern(4);

            else if (Vector2.Distance(Player.transform.position, transform.position) < 30.0f && (BossAttack > AttackTime[4]) && UsePattern[4])
                Pattern(5);
        }
        else
            ani.SetFloat("MoveSpeed", 0.0f);
    }
    void Pattern(int num)
    {
        Attack = true;
        Vector3 Target;

        if (num < 5)
        {
            if (num == 2 || num == 4)
                Effect.SetActive(true);

            agent.destination = transform.position;
            Target = transform.position;
            UsePattern[num - 1] = false;
        }
        else
        {
            Effect.SetActive(true);
            Target = Player.transform.position;
            agent.destination = Target;

            for (int i = 0; i < UsePattern.Length; i++)
                UsePattern[i] = true;
        }
        ani.SetInteger("Attack", num);
        num -= 1;

        BossAttack -= AttackTime[num]; 
        Painting_Mesh[num].transform.position = Target;
        Painting_Mesh[num].transform.rotation = transform.rotation;
        Painting_Mesh[num].gameObject.SetActive(true);
    }

    IEnumerator ResetAttack()
    {
        ani.SetInteger("Attack", 0);
        yield return new WaitForSeconds(1.0f);
        Attack = false; 
    }
}

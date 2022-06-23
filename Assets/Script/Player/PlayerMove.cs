using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    NavMeshAgent agent;
    Animator ani;
    bool Rolling;

    // Start is called before the first frame update
    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        Rolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerStat>().Get_State == PlayerStat.PlayerState.Dead)
        {
            ani.SetBool("Dead", true);
            return;
        }

        if (Rolling)
        {
            //transform.LookAt(agent.destination * 4.0f);
            transform.position = Vector3.Lerp(transform.position, agent.destination, Time.deltaTime);
            return;
        }

        float v = Input.GetAxis("Vertical"); //앞뒤
        float h = Input.GetAxis("Horizontal"); //좌우

        if(v == 0 && h == 0)
        {
            ani.SetFloat("MoveSpeed", 0.0f);
            ani.SetFloat("LeftRight", 0.0f);
        }

        if (v != 0 || h != 0)
        {
            agent.destination = transform.position + (Vector3.right * h + Vector3.forward * v);
            float angle = Mathf.Atan2(agent.destination.x - transform.position.x, agent.destination.z - transform.position.z) * Mathf.Rad2Deg - transform.rotation.eulerAngles.y;

            if (-60.0f < angle && angle < 60.0f)
                MoveForward();

            else if (angle > 120.0f || angle < -120.0f)
                MoveBack();

            if (angle > 45.0f && 135.0f > angle)
                MoveRight();

            else if (angle < -45.0f && -135.0f < angle)
                MoveLeft();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ani.SetBool("Rolling", true);
                agent.destination = transform.position + ((Vector3.right * h + Vector3.forward * v)) * 8.0f;
                Rolling = true;
            }

            if ((angle < 5.0f && angle > -5.0f) || (angle > 175.0f || angle < -175.0f))
            {
                Debug.Log("좌우잠금");
                ani.SetFloat("LeftRight", 0.0f);
            }

            if ((angle < 95.0f && angle > 85.0f) || (angle > -95.0f && angle < -85.0f))
            {
                Debug.Log(angle);
                Debug.Log("앞뒤잠금");
                ani.SetFloat("MoveSpeed", 0.0f);
            }
        }
    }

    void MoveForward()
    {
        ani.SetFloat("MoveSpeed", Mathf.Lerp(ani.GetFloat("MoveSpeed"), 1, Time.deltaTime));
    }
    void MoveBack()
    {
        ani.SetFloat("MoveSpeed", Mathf.Lerp(ani.GetFloat("MoveSpeed"), -1, Time.deltaTime));
    }
    void MoveRight()
    {
        ani.SetFloat("LeftRight", Mathf.Lerp(ani.GetFloat("LeftRight"), 1, Time.deltaTime));
    }
    void MoveLeft()
    {
        ani.SetFloat("LeftRight", Mathf.Lerp(ani.GetFloat("LeftRight"), -1, Time.deltaTime));
    }

    void WakeUp()
    {
        Rolling = false;
        ani.SetBool("Rolling", false);
    }
    public bool GetRolling{get { return Rolling; } }

}

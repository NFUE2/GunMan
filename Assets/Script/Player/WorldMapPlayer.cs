using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldMapPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject Button;

    NavMeshAgent agent;
    Animator ani;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        transform.position = GameManager.GetInstance.GS_JoinStage;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ScreenRay = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.GetMouseButtonUp(0) && Physics.Raycast(ScreenRay,out RaycastHit Hit))
        {
            agent.destination = Hit.point;
            if (Hit.collider.gameObject.layer == LayerMask.NameToLayer("Stage") && Hit.collider.GetComponent<StageState>().GS_Open)
            {
                GameManager.GetInstance.GS_JoinStage = Hit.point;
                GameManager.GetInstance.GS_Stage = Hit.transform.gameObject.GetComponent<StageState>().IngameDate;
                Button.SetActive(true);
            }
            else Button.SetActive(false);
        }

        if(Vector3.Distance(transform.position,agent.destination) > 0.5f)
            ani.SetFloat("MoveSpeed", 1.0f);
        else
            ani.SetFloat("MoveSpeed", 0.0f);
    }
}

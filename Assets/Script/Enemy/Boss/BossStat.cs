using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : MonoBehaviour
{
    

    public enum State
    {
        Alive,
        Dead,
    }

    State BossState;

    [SerializeField, Range(1.0f, 1000.0f)]
    float Hp;

    // Start is called before the first frame update
    void Start()
    {
        BossState = State.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0.0f)
        {
            BossState = State.Dead;
            return;
        }
    }

    public float Set_Hp { set { Hp -= value; } }
    public State Get_State { get { return BossState; } }
}

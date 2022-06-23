using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    GameObject Ui;

    public enum PlayerState
    {
        Alive,
        Dead,
    }
    PlayerState State;

    int Hp;
    // Start is called before the first frame update
    private void Awake()
    {
        Hp = 5;
        
    }
    void Start()
    {
        State = PlayerState.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
            State = PlayerState.Dead;
    }

    public int Get_Hp{get { return Hp; }}
    public PlayerState Get_State { get { return State; } }
    public int damage {
        set
        {
            if (Hp > 0)
            {
                Hp -= value;
                Ui.GetComponent<PlayerUi>().Damage();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    GameObject Clear;

    [SerializeField]
    GameObject Lose;

    private List<GameObject> Enemy;

    private static ObjectManager Instance;

    public static ObjectManager GetInstance
    {
        get
        {
            return Instance;
        }
    }

    private void OnEnable()
    {
        Instance = this;
        Enemy = new List<GameObject>();
    }

    private void Start()
    {
        GameObject Stage = Instantiate(GameManager.GetInstance.GS_Stage);
        Stage.transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (GetInstance.EnemyCount == 0)
            Clear.SetActive(true);

        else if ((GameObject.Find("Player").GetComponent<PlayerStat>().Get_State == PlayerStat.PlayerState.Dead))
            Lose.SetActive(true);
    }

    public void SetGameObject(GameObject Gobj)
    {
        //리스트에서 게임오브젝트를 추가
        Enemy.Add(Gobj);
    }

    public void DeleteGameObject(GameObject Gobj)
    {
        //리스트에서 게임오브젝트를  찾아서 제거함.
        if (Enemy.Contains(Gobj))
            Enemy.Remove(Gobj);
    }

    public int EnemyCount { get { return Enemy.Count; } } //현재 게임오브젝트가 몇개 들어있는지 확인하는 프로퍼티
}

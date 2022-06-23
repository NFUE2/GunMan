using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    AudioClip FireSound;

    [SerializeField]
    GameObject GunUi;

    [SerializeField, Range(0.0f, 10.0f)]
    float Delay;

    float AttackDelay;
    float ReloadingTime;
    bool Reloading;

    RaycastHit Hit;

    [SerializeField]
    GameObject BulletType;

    List<GameObject> LoadMagazine;
    List<GameObject> ReLoadMagazine;

    [SerializeField]
    GameObject[] Effect;

    Animator ani;

    Vector3 Direction;

    [SerializeField]
    Transform FirePosition;

    float OffsetAngle;
    // Start is called before the first frame update
    void Start()
    {
        LoadMagazine = new List<GameObject>();
        ReLoadMagazine = new List<GameObject>();

        AttackDelay = Delay;
        Reloading = false;
        ani = GetComponent<Animator>();
        ReloadingTime = 0.0f;

        if (GameObject.Find("Magazine") == null)
            new GameObject("Magazine");

        for (int i = 0; i < 6; i++)
        {
            GameObject Bullet = Instantiate(BulletType);
            Bullet.name = "" + (i +1);
            Bullet.SetActive(false);
            Bullet.AddComponent<AudioSource>();
            Bullet.GetComponent<AudioSource>().clip = FireSound;
            Bullet.transform.SetParent(GameObject.Find("Magazine").transform);

            LoadMagazine.Add(Bullet);
        }


    }

    private void Update()
    {
        if (GetComponent<PlayerStat>().Get_State == PlayerStat.PlayerState.Dead)
            return;

        if (AttackDelay < Delay && !Reloading)
        {
            AttackDelay += Time.deltaTime;
        }
        else if (Reloading)
        {
            if (ReloadingTime < 3.0f)
            {
                ReloadingTime += Time.deltaTime;
                GunUi.GetComponent<PlayerUi>().SetFill = ReloadingTime / 3.0f;
            }
            else
            {
                Reload();
                ReloadingTime = 0.0f;
                Reloading = false;
            }

            AttackDelay = 0.0f;
        }

        if(GetComponent<PlayerMove>().GetRolling)
            return;

        else
        {
            Ray ScreenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            foreach (RaycastHit Hit in Physics.RaycastAll(ScreenRay, 50.0f))
            {
                if (Hit.collider.gameObject.layer == 8)
                {
                    if ((Input.GetMouseButtonDown(0) && AttackDelay >= Delay) && LoadMagazine.Count > 0)
                    {
                        ani.SetTrigger("Attack");
                        Attack(Hit.point + new Vector3(0.0f,FirePosition.position.y,0.0f));
                        for (int i = 0; i < Effect.Length; i++)
                        {
                            if(Effect[i].activeSelf == false)
                            {
                                Effect[i].SetActive(true);
                                break;
                            }
                        }
                    }
                    else if (LoadMagazine.Count == 0 && !Reloading)
                        Reloading = true;
                    transform.LookAt(Hit.point);
                }
                else continue;
            }
        }
    }
    // Update is called once per frame
    public void Attack(Vector3 Direction)
    {
        if (LoadMagazine.Count > 0 && AttackDelay >= Delay)
        {
            GunUi.GetComponent<PlayerUi>().Fire();

            GameObject Obj = LoadMagazine[0];
            Obj.GetComponent<Bullet>().Set_Origin = transform.position;
            Obj.GetComponent<Bullet>().Set_Direction = Direction - FirePosition.position;
            Obj.transform.position = FirePosition.position;


            Obj.SetActive(true);

            ReLoadMagazine.Add(LoadMagazine[0]);
            LoadMagazine.Remove(LoadMagazine[0]);
            AttackDelay = 0.0f;
        }
       
    }

    private void Reload()
    {
        while(ReLoadMagazine.Count > 0)
        {
            LoadMagazine.Add(ReLoadMagazine[0]);
            ReLoadMagazine.Remove(ReLoadMagazine[0]);
        }
        Reloading = false;
    }

    public GameObject Set_Bullet { set { BulletType = value; } }
}

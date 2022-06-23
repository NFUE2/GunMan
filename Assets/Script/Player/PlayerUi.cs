using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    Image Gun;
    int ReloadingSize;
    float Reloading;

    [SerializeField]
    GameObject[] Ammo;

    [SerializeField]
    GameObject Hp;

    GameObject Player;
    PlayerStat PS;

    [SerializeField]
    Sprite Perfect_Heart;

    List<GameObject> Hpcount;

    // Start is called before the first frame update
    void Start()
    {
        Hpcount = new List<GameObject>();

        ReloadingSize = 0;
        Reloading = 0.0f;
        Gun = transform.GetChild(0).GetComponent<Image>();

        Player = GameObject.Find("Player");
        PS = Player.GetComponent<PlayerStat>();

        for (int i = 0; i < PS.Get_Hp; i++)
        {
            GameObject Heart = new GameObject("Heart");
            Hpcount.Add(Heart);

            Heart.AddComponent<Image>();
            Heart.transform.SetParent(Hp.transform);

            Heart.GetComponent<Image>().sprite = Perfect_Heart;
        }
    }

    public void Fire()
    {
        if(ReloadingSize != 6)
        {
            Ammo[ReloadingSize].SetActive(false);
            ReloadingSize++;
        }
    }

    private void Reload()
    {
        Gun.fillAmount = Reloading;

        if (Reloading > 1.0f)
        {
            Gun.fillAmount = 1.0f;
            for (int i = 0; i < 6; i++)
            {
                Ammo[i].SetActive(true);
                ReloadingSize = 0;
            }
        }
    }

    public void Damage()
    {
        for (int i = 4; i > PS.Get_Hp - 1; i--)
        {
            if (!Hpcount[i].activeSelf)
                continue;

            Hpcount[i].SetActive(false);
        }
    }

    public float SetFill { set { Reloading = value; Reload(); } }
}

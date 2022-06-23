using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class StickController : MonoBehaviour
{
    [SerializeField]
    RectTransform LeftStick;

    [SerializeField]
    RectTransform LeftStickController;

    [SerializeField]
    RectTransform RightStick;

    [SerializeField]
    RectTransform RightStickController;

    [SerializeField]
    GameObject Player;

    float speed;
    bool rockon;

     private float leftRadius;
     private float rightRadius;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10.0f;
        leftRadius = (int)LeftStick.rect.height >> 1;
        rightRadius = (int)RightStick.rect.height >> 1;
        rockon = false;
    }

    // Update is called once per frame
    void Update()
    {
        //좌클릭시 클릭한 위치에 따라 조이스틱이 움직이는 코드
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < Screen.width >> 1)
            {
                LeftStick.position = Input.mousePosition;
            }
            else if (Input.mousePosition.x < Screen.width << 1)
            {
                RightStick.position = Input.mousePosition;
                rockon = true;
            }
        }

        //좌클릭중
        if(Input.GetMouseButton(0))
        {
            if(Input.mousePosition.x < Screen.width >> 1 )
            {
                LeftStickController.position = Input.mousePosition;
                LeftStickController.localPosition = Vector2.ClampMagnitude(LeftStickController.localPosition, leftRadius);
                Moving();
            }
            else if(Input.mousePosition.x > Screen.width >> 1)
            {
                RightStickController.position = Input.mousePosition;
                RightStickController .localPosition = Vector2.ClampMagnitude(RightStickController.localPosition, rightRadius);

                if (Vector2.Distance(RightStick.position, RightStickController.position) / rightRadius > 0.75f)
                    //Player.GetComponent<PlayerFire>().Attack();

                if (rockon) RockOn();
            }
        }

        //좌클릭 종료시 좌 또는 우중에서 클릭한 면의 조이스틱이 원점으로 복귀
        if(Input.GetMouseButtonUp(0))
        {
            if(Input.mousePosition.x < Screen.width >> 1)
            {
                LeftStick.localPosition = Vector2.zero;
                LeftStickController.localPosition = Vector2.zero;
            }
            if (Input.mousePosition.x > Screen.width >> 1)
            {
                RightStick.localPosition = Vector2.zero;
                RightStickController.localPosition = Vector2.zero;
                rockon = false;
            }
        }
    }



    void Moving()
    {
        Vector3 dir = LeftStickController.position - LeftStick.position;

        if(!rockon)
        {
            Vector3 rock = LeftStickController.position - LeftStick.position;
            float angle = Mathf.Atan2(rock.x, rock.y) * Mathf.Rad2Deg;
            Player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
        Player.GetComponent<NavMeshAgent>().destination = Player.transform.position + new Vector3(dir.x, 0, dir.y).normalized;

        //Player.transform.position += new Vector3(dir.x, 0, dir.y).normalized * speed * Time.deltaTime;
    }
    void RockOn()
    {
        Vector3 dir = RightStickController.position - RightStick.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        Player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}


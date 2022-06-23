using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPattern : MonoBehaviour
{
    [SerializeField, Range(0.0f , 100.0f)]
    float Size;

    [SerializeField, Range(0.0f, 100.0f)]
    float Angle;



    //X자 피자패턴 
    public void Pattern1()
    {
        StartCoroutine(Pizza());
    }

    IEnumerator Pizza()
    {
        foreach(Collider col in Physics.OverlapSphere(transform.position,Size))
        {
            if (col.name == "Player")
            {
                float TargetAngle = Vector3.Angle(transform.forward,col.transform.position - transform.position);

                for (int i = 0; i < 3; i++)
                {
                    float min = (-15.0f) + (i * 90.0f) < 0.0f ? 0.0f : (-15.0f) + (i * 90.0f);

                    if (TargetAngle > min && TargetAngle < 30.0f + (i * 90.0f))
                    {
                        //col.GetComponent<PlayerStat>().
                    }
                }
            }
        }
        yield return new WaitForSeconds(1.0f);
    }


    //원형 장판 데미지 패턴 
    public void Pattern2()
    {
        StartCoroutine(Circle());
    }

    IEnumerator Circle()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, Size))
        {
            if(col.name =="Player")
            {
                //col.GetComponent<PlayerStat>().
            }
        }

        yield return new WaitForSeconds(1.0f);
    }

    private void Update()
    {
    }


    //방향이 바뀌는 피자 패턴
    public void Pattern3()
    {
        //StartCoroutine(RotationPizza());
    }

    IEnumerator RotationPizza()
    {
        //foreach(Collider col in Physics.OverlapSphere(transform.position,Size))
        //{
        //    if (col.name == "Player")
        //    {
        //        for (int i = 0; i < 6; i++)
        //        {
        //            float TargetAngle = Quaternion.FromToRotation(-transform.right, col.transform.position - transform.position).eulerAngles.y;

        //            yield return new WaitForSeconds(3.0f);
        //            Debug.Log(TargetAngle);
        //            if (TargetAngle > i * 60 && TargetAngle < 60.0f + (i * 60.0f))
        //            {

        //                Debug.Log("적중" + i);
        //                //col.GetComponent<PlayerStat>().
        //            }
        //        }
        //    }
        //}
        yield return new WaitForSeconds(3.0f);
    }



    ////바닥 브레스 패턴
    //public void Pattern4()
    //{

    //}

    //도넛패턴
    public void Pattern5()
    {
        StartCoroutine(Donut());
    }

    IEnumerator Donut()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, Size))
        {
            if(col.name == "Player" && Vector3.Distance(transform.position,col.transform.position) > Size / 2)
            {
                //col.GetComponent<PlayerStat>().
            }
        }
        yield return new WaitForSeconds(1.0f);
    }
    //public void Pattern6()
    //{
    //    StartCoroutine(Wave());
    //}

    ////점점 커지는 부채꼴패턴 - 안쓸듯
    //IEnumerator Wave()
    //{
    //    for (int i = 1; i <= 6; i++)
    //    {
    //        foreach(Collider col in Physics.OverlapSphere(transform.position,5.0f * i))
    //        {
    //            float Angle = Vector3.Angle(transform.forward,col.transform.position - transform.position);
    //            float Distance = Vector3.Distance(transform.position,col.transform.position);

    //            if(Angle < 30.0f && Distance > 5.0f * (i - 1))
    //            {

    //            }
    //        }
    //    }
    //    yield return new WaitForSeconds(1.0f);
    //}
    
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;

        DrawGizmo.DrawCircle(transform.position,10.0f);

        //피자 패턴 그림
        //{
        //    Gizmos.DrawWireSphere(transform.position, 10.0f);
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Gizmos.DrawLine(transform.position, transform.position + LimitLine(-Angle + (i * 90)) * Size);
        //        Gizmos.DrawLine(transform.position, transform.position + LimitLine(Angle + (i * 90)) * Size);
        //    }
        //}

        //회전피자 패턴
        //Gizmos.DrawWireSphere(transform.position, Size);

        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawLine(transform.position, transform.position + LimitLine(-90 + (i * 60)) * Size);
        }



        ////십자 장판 그림
        //Gizmos.DrawWireCube(transform.position + new Vector3(0.0f,0.0f,10.0f), new Vector3(30.0f,0.0f,10.0f));
        //Gizmos.DrawWireCube(transform.position + new Vector3(0.0f, 0.0f, 10.0f), new Vector3(10.0f, 0.0f, 30.0f));

        //도넛 패턴 그림
        //Gizmos.DrawWireSphere(transform.position, Size);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, Size/2);

    }

    Vector3 LimitLine(float Angle)
    {
        Angle += transform.rotation.eulerAngles.y;
        return new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad),0.0f,Mathf.Cos(Angle * Mathf.Deg2Rad));
    }

}

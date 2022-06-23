using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Circle_Pattern : MonoBehaviour
{
    [SerializeField]
    GameObject Effect;

    Vector3[] Circle_vertices;
    int[] Circle_triangles;

    [Tooltip("패턴이 반짝일 시간")]
    [SerializeField, Range(0.1f, 10.0f)]
    float FlashTime;

    [Tooltip("원의 반지름")]
    [SerializeField, Range(10.0f, 180.0f)]
    float radius;
    float PatternTime;

    Mesh mesh;
    MeshFilter MF;
    MeshRenderer MR;

    private void Awake()
    {
        mesh = new Mesh();
        MF = GetComponent<MeshFilter>();
        MR = GetComponent<MeshRenderer>();

        Circle_vertices = new Vector3[75];
        Circle_triangles = new int[75];

        for (int i = 0; i <= 73; i++)
        {
            if (i % 3 == 0)
            {
                Circle_vertices[i] = Vector3.zero;
                Circle_triangles[i] = 0;
                continue;
            }

            else if (i % 3 == 1 && i > 3)
                Circle_vertices[i] = Circle_vertices[i - 2];

            else if (i % 3 == 2 || (i % 3 == 1 && i < 3))
            {
                float angle = (i * 5.0f) * Mathf.Deg2Rad;
                Circle_vertices[i] = new Vector3(Mathf.Sin(angle), 0.01f, Mathf.Cos(angle)) * radius;
            }

            Circle_triangles[i] = i;
        }

        Circle_vertices[74] = Circle_vertices[1];
        Circle_triangles[74] = 74;

        mesh.vertices = Circle_vertices;
        mesh.triangles = Circle_triangles;
        MF.mesh = mesh;
        gameObject.SetActive(false);
    }

    void GroundCrack()
    {
        Effect.SetActive(true);
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            if (col.name == "Player")
                col.GetComponent<PlayerStat>().damage = 2;
        }
    }
    void AttackEnd()
    {
        gameObject.SetActive(false);
        PatternTime = 0.0f;
    }
}

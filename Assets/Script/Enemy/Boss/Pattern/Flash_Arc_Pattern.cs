using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Arc_Pattern : MonoBehaviour
{
    [SerializeField]
    GameObject[] Effect;

    [Range(10.0f, 360.0f)]
    public float Arc_angle;

    public Vector3[] Arc_vertices;
    int[] Arc_triangles;
    public Vector3[] vertices_Origin;

    [Tooltip("원의 반지름")]
    [Range(10.0f, 180.0f)]
    public float radius;

    Mesh mesh;
    MeshFilter MF;
    MeshRenderer MR;

    private void Awake()
    {
        mesh = new Mesh();
        MF = GetComponent<MeshFilter>();
        MR = GetComponent<MeshRenderer>();

        Arc_vertices = new Vector3[60];
        Arc_triangles = new int[60];

        for (int i = 0; i < 60; i++)
        {
            if (i % 3 == 0)
            {
                Arc_vertices[i] = Vector3.zero;
                Arc_triangles[i] = 0;

                continue;
            }

            else if (i % 3 == 1 && i > 3)
                Arc_vertices[i] = Arc_vertices[i - 2];

            else if (i % 3 == 2 || (i % 3 == 1 && i < 3))
            {
                float angle = ((i * (Arc_angle / 60.0f) - Arc_angle / 2)) * Mathf.Deg2Rad;
                Arc_vertices[i] = new Vector3(Mathf.Sin(angle), 0.01f, Mathf.Cos(angle)) * radius;
            }

            Arc_triangles[i] = i;
        }

        vertices_Origin = Arc_vertices.Clone() as Vector3[];

        mesh.vertices = Arc_vertices;
        mesh.triangles = Arc_triangles;
        MF.mesh = mesh;

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Arc_vertices = vertices_Origin.Clone() as Vector3[];
    }

    void Hit()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
            if (col.name == "Player" && Vector3.Angle(transform.forward, col.transform.position - transform.position) < (Arc_angle / 2))
                col.GetComponent<PlayerStat>().damage = 1;
    }
    void End()
    {
        gameObject.SetActive(false);
    }


    
}

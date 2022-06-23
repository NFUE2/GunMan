using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill_Circle_Pattern : MonoBehaviour
{
    public Vector3[] Circle_vertices;
    int[] Circle_triangles;
    public Vector3[] vertices_Origin;

    [Tooltip("시전시간")]
    [SerializeField, Range(0.1f, 100.0f)]
    float AttackTime;

    float radius;

    float PatternTime;

    Mesh mesh;
    MeshFilter MF;

    private void Awake()
    {
        mesh = new Mesh();
        MF = GetComponent<MeshFilter>();

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
                Circle_vertices[i] = new Vector3(Mathf.Sin(angle), 0.01f, Mathf.Cos(angle));
            }

            Circle_triangles[i] = i;
        }

        Circle_vertices[74] = Circle_vertices[1];
        Circle_triangles[74] = 74;

        vertices_Origin = Circle_vertices.Clone() as Vector3[];

        mesh.vertices = Circle_vertices;
        mesh.triangles = Circle_triangles;
        MF.mesh = mesh;

        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Circle_vertices = vertices_Origin.Clone() as Vector3[];
    }

    void Update()
    {
        if (PatternTime < AttackTime)
        {
            PatternTime += Time.deltaTime;

            for (int i = 1; i < Circle_vertices.Length; i++)
            {
                Circle_vertices[i] += Circle_vertices[i] * Time.deltaTime * 2;
            }
            mesh.vertices = Circle_vertices;
            MF.mesh = mesh;
        }
        else
        {
            PatternTime = 0.0f;
            gameObject.SetActive(false);
        }
    }
}

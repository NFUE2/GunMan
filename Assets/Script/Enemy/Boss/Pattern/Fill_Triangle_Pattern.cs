using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill_Triangle_Pattern : MonoBehaviour
{
    Vector3[] triangle_vertices;
    int[] triangles_triangles;
    Vector3[] vertices_Origin;

    [Tooltip("시전시간")]
    [SerializeField, Range(0.1f, 100.0f)]
    float AttackTime;

    [Tooltip("angle*2 = 범위")]
    [SerializeField, Range(10.0f, 180.0f)]
    float angle;

    float PatternTime;

    Mesh mesh;
    MeshFilter MF;

    private void Awake()
    {
        PatternTime = 0.0f;
        mesh = new Mesh();

        vertices_Origin = new Vector3[3];
        triangle_vertices = new Vector3[3];
        triangles_triangles = new int[3] { 0, 1, 2 };

        MF = GetComponent<MeshFilter>();

        triangle_vertices[0] = transform.position + new Vector3(0.0f, 0.01f, 0.0f);

        for (int i = 1; i <= 2; i++)
        {
            float seta = (-angle + (i - 1) * (angle * 2)) * Mathf.Deg2Rad;
            triangle_vertices[i] = new Vector3(Mathf.Sin(seta), 0.01f, Mathf.Cos(seta));
        }
        vertices_Origin = triangle_vertices.Clone() as Vector3[];

        mesh.vertices = triangle_vertices;
        mesh.triangles = triangles_triangles;
        MF.mesh = mesh;

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        triangle_vertices = vertices_Origin.Clone() as Vector3[];
    }

    // Update is called once per frame
    void Update()
    {
        if (PatternTime < AttackTime)
        {
            PatternTime += Time.deltaTime;

            for (int i = 1; i < triangle_vertices.Length; i++)
            {
                triangle_vertices[i] += triangle_vertices[i] * Time.deltaTime * 4;
            }
            mesh.vertices = triangle_vertices;
            MF.mesh = mesh;
        }
        else
        {
            PatternTime = 0.0f;
            mesh.vertices = vertices_Origin;
            triangle_vertices = vertices_Origin;
            MF.mesh = mesh;
            gameObject.SetActive(false);
        }
    }
}

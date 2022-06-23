using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    Vector3[] circle_vertices;
    int[] circle_triangles;

    Vector3[] triangle_vertices;
    int[] triangles_triangles;

    float PatternTime;
    Mesh mesh;
    MeshFilter MF;

    private void Awake()
    {
        PatternTime = 1.0f;
        mesh = new Mesh();

        triangle_vertices = new Vector3[3];
        triangles_triangles = new int[3]{ 0 , 1 , 2};

        MF = GetComponent<MeshFilter>();

        triangle_vertices[0] = transform.position + new Vector3(0.0f,0.01f,0.0f);

        for (int i = 1; i <= 2; i++)
        {
            float seta = (-60.0f + (i - 1) * 120) * Mathf.Deg2Rad;
            triangle_vertices[i] = new Vector3(Mathf.Sin(seta),0.01f,Mathf.Cos(seta)) * 100.0f;
        }

        mesh.vertices = triangle_vertices;
        mesh.triangles = triangles_triangles;
        MF.mesh = mesh;

        //circle_vertices = new Vector3[12];
        //triangles_triangles = new int[12];

        //vertices[0] = Vector3.zero;
        //triangles[0] = 0;

        //for (int i = 1; i < 12; i++)
        //{
        //    float seta = (i * Mathf.Deg2Rad) * 30.0f;
        //    vertices[i] = new Vector3(Mathf.Sin(seta), 0.0f, Mathf.Cos(seta));

        //    triangles[i] = i;
        //}

        //mesh.vertices = vertices;
        //mesh.triangles = triangles;
        //MF.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PatternTime < 2.0f)
            PatternTime += Time.deltaTime;
        else
            gameObject.SetActive(false);
    }
}

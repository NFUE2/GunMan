using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill_Box_Pattern : MonoBehaviour
{
    [SerializeField]
    GameObject Effect;

    Vector3[] Box_vertices;
    int[] Box_triangles;
    Vector3[] vertices_Origin;

    [Tooltip("시전시간")]
    [SerializeField, Range(0.1f, 100.0f)]
    float AttackTime;

    [Tooltip("너비")]
    [SerializeField, Range(0.1f, 180.0f)]
    float width;

    [Range(1.0f,50.0f)]
    public float length;


    float PatternTime;

    Mesh mesh;
    MeshFilter MF;
    // Start is called before the first frame update

    private void Awake()
    {
        PatternTime = 0.0f;
        mesh = new Mesh();
        MF = GetComponent<MeshFilter>();

        vertices_Origin = new Vector3[4];
        Box_vertices = new Vector3[4] {
            Vector3.left * width,
            Vector3.right * width,
            Vector3.left * width + Vector3.forward ,
            Vector3.right * width + Vector3.forward
        };

        for (int i = 0; i < Box_vertices.Length; i++)
            Box_vertices[i] += new Vector3(0.0f, 0.01f, 0.0f);

        Box_triangles = new int[6] { 0, 2, 3, 0, 3, 1 };

        vertices_Origin = Box_vertices.Clone() as Vector3[];

        mesh.vertices = Box_vertices;
        mesh.triangles = Box_triangles;
        MF.mesh = mesh;

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Box_vertices = vertices_Origin.Clone() as Vector3[];
        mesh.vertices = Box_vertices;
        MF.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        if (PatternTime < AttackTime)
        {
            PatternTime += Time.deltaTime;

            for (int i = 2; i < Box_vertices.Length; i++)
            {
                if(Vector3.Distance(Box_vertices[i - 2], Box_vertices[i - 2] + Box_vertices[i]) < length)
                    Box_vertices[i] += Vector3.forward * Time.deltaTime * 10;
            }
            mesh.vertices = Box_vertices;
            MF.mesh = mesh;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            other.GetComponent<PlayerStat>().damage = 2;
    }
    //void Hit()
    //{
    //    Vector3 Center = new Vector3(0.0f, 0.0f,length / 2);
    //    Vector3 HalfSize = new Vector3(width * 2, 2, length);

    //    foreach (Collider col in Physics.OverlapBox(Center, HalfSize))
    //        if (col.name == "Player")
    //            col.GetComponent<PlayerStat>().damage = 2;
    //}

    void End()
    {
        gameObject.SetActive(false);
    }
}

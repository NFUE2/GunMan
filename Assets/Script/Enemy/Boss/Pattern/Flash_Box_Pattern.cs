using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Box_Pattern : MonoBehaviour
{
    [SerializeField]
    GameObject Effect;

    Vector3[] Box_vertices;
    int[] Box_triangles;

    [Tooltip("패턴이 반짝일 시간")]
    [SerializeField, Range(0.1f, 10.0f)]
    float FlashTime;

    [Tooltip("너비")]
    [SerializeField, Range(0.1f, 180.0f)]
    float width;

    [Tooltip("길이")]
    [Range(10.0f, 180.0f)]
    public float length;
    float PatternTime;

    Mesh mesh;
    MeshFilter MF;
    MeshRenderer MR;
    // Start is called before the first frame update

    private void Awake()
    {
        mesh = new Mesh();
        MF = GetComponent<MeshFilter>();
        MR = GetComponent<MeshRenderer>();

        Box_vertices = new Vector3[4] {
            Vector3.left * width,
            Vector3.right * width,
            Vector3.left * width + Vector3.forward * length ,
            Vector3.right * width + Vector3.forward * length
        };

        for (int i = 0; i < Box_vertices.Length; i++)
            Box_vertices[i] += new Vector3(0.0f, 0.01f, 0.0f);

        Box_triangles = new int[6] { 0, 2, 3, 0, 3, 1 };

        mesh.vertices = Box_vertices;
        mesh.triangles = Box_triangles;
        MF.mesh = mesh;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            other.GetComponent<PlayerStat>().damage = 1;
    }

    //void Hit()
    //{
    //    //Vector3 Center = new Vector3(0.0f, 0.0f,length / 2);
    //    //Vector3 HalfSize = new Vector3(width * 2, 2, length);

    //    //foreach (Collider col in Physics.OverlapBox(Center, HalfSize))
    //    //    if (col.name == "Player")
    //    //        col.GetComponent<PlayerStat>().damage = 1;
    //}

    void End()
    {
        gameObject.SetActive(false);
    }
}

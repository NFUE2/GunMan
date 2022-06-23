using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Texture2D CursorImange;

    AudioSource AS;
    int ClearCount;
    Vector3 JoinStage;

    GameObject Stage;

    private static GameManager Instance;
    public static GameManager GetInstance { get { return Instance; } }

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 40; 
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        ClearCount = 0;
        JoinStage = Vector3.zero;
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(CursorImange,Vector2.zero,CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Confined;

        if(SceneManager.GetActiveScene().name == "InGame")
            AS.enabled = false;
        else
            AS.enabled = true;

    }

    public GameObject GS_Stage { get { return Stage; } set { Stage = value; } }
    public Vector3 GS_JoinStage { get { return JoinStage; } set { JoinStage = value; } }
    public int StageClear{ get { return ClearCount; } set { ClearCount++; } }
}

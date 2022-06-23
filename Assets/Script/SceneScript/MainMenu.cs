using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject Text;

    TextMeshProUGUI Target;
    bool Effect;
    
    // Start is called before the first frame update
    void Start()
    {
        Target = Text.GetComponent<TextMeshProUGUI>();
        Effect = false;
    }

    // Update is called once per frame
    void Update()
    {
        //깜빡깜빡거리는 것 같은 효과
        if (Target.color.a < 0) Effect = true;
        else if (Target.color.a > 1) Effect = false;

        if (Effect)
            Target.color += new Color(0, 0, 0, Time.deltaTime);
        else
            Target.color -= new Color(0, 0, 0, Time.deltaTime);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("WorldMap");
    }
}

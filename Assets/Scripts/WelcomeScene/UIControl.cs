using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour {

    public GUIStyle styleBtnStart;
    public GUIStyle styleBtnReset;
    public GUIStyle styleBtnAchievement;
    public GUIStyle styleBtnSettings;

    Vector2 sizeButton;
    Vector2 posBtnStart, posBtnReset, posBtnAchievement, posBtnSettings;


    void Awake()
    {
        sizeButton = new Vector2(Screen.width * 0.4f, Screen.height * 0.08f);

        posBtnStart = new Vector2(Screen.width * 0.05f, Screen.height * 0.73f);
        posBtnReset = new Vector2(Screen.width * 0.55f, Screen.height * 0.73f);
        posBtnAchievement = new Vector2(Screen.width * 0.05f, Screen.height * 0.86f);
        posBtnSettings = new Vector2(Screen.width * 0.55f, Screen.height * 0.86f);
    }

    void OnGUI()
    {
        GUI.Button(new Rect(posBtnStart, sizeButton)," ", styleBtnStart);

        if (GUI.Button(new Rect(posBtnReset, sizeButton), " ", styleBtnReset)) {
            SceneManager.LoadScene("Reset", LoadSceneMode.Single);
        }

        if (GUI.Button(new Rect(posBtnAchievement, sizeButton), " ", styleBtnAchievement))
        {
            SceneManager.LoadScene("Achievement", LoadSceneMode.Single);
        }

        if (GUI.Button(new Rect(posBtnSettings, sizeButton), " ", styleBtnSettings))
        {
            SceneManager.LoadScene("Settings", LoadSceneMode.Single);
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    public GUIStyle btnStart;
    public GUIStyle btnReset;
    public GUIStyle btnAchievement;
    public GUIStyle btnSettings;


    void OnGUI() {
        GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.7f, Screen.width * 0.4f, Screen.width * 0.8f), "Start", btnStart);

    }
}

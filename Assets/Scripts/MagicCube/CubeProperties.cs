using UnityEngine;
using System.Collections;

public class CubeProperties : MonoBehaviour {

    Hashtable color;

    void Awake()
    {
        initSelf();
    }

    public void setColor(string plane, int colorIndex)
    {
        color[plane] = colorIndex;
    }

    public bool checkColor(string plane, int colorIndex)
    {
        return (int)color[plane] == colorIndex;
    }

    void initSelf()
    {
        color = new Hashtable();
        color.Add("Front", -1);
        color.Add("Behind", -1);
        color.Add("Left", -1);
        color.Add("Right", -1);
        color.Add("Up", -1);
        color.Add("Down", -1);
    }

}

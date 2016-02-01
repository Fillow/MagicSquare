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

    public void setColor(int front, int behind, int left, int right, int up, int down)
    {
        color["front"] = front;
        color["behind"] = behind;
        color["left"] = left;
        color["right"] = right;
        color["up"] = up;
        color["down"] = down;
    }

    public bool checkColor(string plane, int colorIndex)
    {
        return (int)color[plane] == colorIndex;
    }

    void initSelf()
    {
        color = new Hashtable();
        color.Add("front", -1);
        color.Add("behind", -1);
        color.Add("left", -1);
        color.Add("right", -1);
        color.Add("up", -1);
        color.Add("down", -1);
    }

}

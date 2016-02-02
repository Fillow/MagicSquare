using UnityEngine;
using System.Collections;


public class CubeProperties : MonoBehaviour {

    Hashtable color;
    Transform transformOfPresentCube;

    void Awake()
    {
        initSelf();
    }

    public void setColor(string plane, int colorIndex)
    {
        color[plane] = colorIndex;
    }

    public void setColor()
    {
        transformOfPresentCube = GetComponent<Transform>();
        switch ((int)transformOfPresentCube.position.x)
        {
            case 1 :
                color["front"] = "001";
                color["behind"] = -1;
                break;
            case 0 :
                color["front"] = -1;
                color["behind"] = -1;
                break;
            case -1 :
                color["front"] = -1;
                color["behind"] = "010";
                break;
        }
        switch ((int)transformOfPresentCube.position.y)
        {
            case 1 :
                color["up"] = "011";
                color["down"] = -1;
                break;
            case 0 :
                color["up"] = -1;
                color["down"] = -1;
                break;
            case -1 :
                color["up"] = -1;
                color["down"] = "100";
                break;
        }
        switch ((int)transformOfPresentCube.position.z)
        {
            case 1 :
                color["right"] = "101";
                color["left"] = -1;
                break;
            case 0 :
                color["right"] = -1;
                color["left"] = -1;
                break;
            case -1 :
                color["right"] = -1;
                color["left"] = "110";
                break;
        }
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
        color.Add("up", -1);
        color.Add("down", -1);
        color.Add("left", -1);
        color.Add("right", -1);
        // char* colorBinaryCodePtr;    //颜色用二进制编码表示，分别为：蓝：001绿：010白：011黄：100红：101橙：110
        // foreach(DictionaryEntry a in color)
        // {
        //     colorBinaryCodePtr = (char *)malloc(sizeof(char) * 3);
        //     a.value = colorBinaryCodePtr;
        // }
    }

}

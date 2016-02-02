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

    //用二进制编码表示面的朝向，分别为：前：001后：010上：011下：100右：101左：110
    public void setColor()
    {
        transformOfPresentCube = GetComponent<Transform>();
        switch ((int)transformOfPresentCube.position.x)
        {
            case 1 :
                color["blue"] = "001";
                color["green"] = -1;
                break;
            case 0 :
                color["blue"] = -1;
                color["green"] = -1;
                break;
            case -1 :
                color["blue"] = -1;
                color["green"] = "010";
                break;
        }
        switch ((int)transformOfPresentCube.position.y)
        {
            case 1 :
                color["white"] = "011";
                color["yellow"] = -1;
                break;
            case 0 :
                color["white"] = -1;
                color["yellow"] = -1;
                break;
            case -1 :
                color["white"] = -1;
                color["yellow"] = "100";
                break;
        }
        switch ((int)transformOfPresentCube.position.z)
        {
            case 1 :
                color["red"] = "101";
                color["orange"] = -1;
                break;
            case 0 :
                color["red"] = -1;
                color["orange"] = -1;
                break;
            case -1 :
                color["red"] = -1;
                color["orange"] = "110";
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
        color.Add("blue", -1);
        color.Add("green", -1);
        color.Add("white", -1);
        color.Add("yellow", -1);
        color.Add("red", -1);
        color.Add("orange", -1);
        // char* colorBinaryCodePtr;   
        // foreach(DictionaryEntry a in color)
        // {
        //     colorBinaryCodePtr = (char *)malloc(sizeof(char) * 3);
        //     a.value = colorBinaryCodePtr;
        // }
    }

}

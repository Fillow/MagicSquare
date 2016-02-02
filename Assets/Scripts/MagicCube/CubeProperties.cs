using UnityEngine;
using System.Collections;


public class CubeProperties : MonoBehaviour {

    Hashtable color;
    Transform transformOfPresentCube;

    void Awake()
    {
        initSelf();
    }

    void initSelf()
    {
        color = new Hashtable();
        color.Add("blue", "-1");
        color.Add("green", "-1");
        color.Add("white", "-1");
        color.Add("yellow", "-1");
        color.Add("red", "-1");
        color.Add("orange", "-1");
        /** Hashtable 机制
            char* colorBinaryCodePtr;   
            foreach(DictionaryEntry a in color)
            {
                colorBinaryCodePtr = (char *)malloc(sizeof(char) * 3);
                a.value = colorBinaryCodePtr;
            }
        */

        /**用二进制编码表示面的朝向:
        前：001
        后：010
        上：011
        下：100
        右：101
        左：110
        */
        transformOfPresentCube = GetComponent<Transform>();
        switch ((int)transformOfPresentCube.position.x)
        {
            case 1:
                color["blue"] = "001";
                color["green"] = "-1";
                break;
            case 0:
                color["blue"] = "-1";
                color["green"] = "-1";
                break;
            case -1:
                color["blue"] = "-1";
                color["green"] = "010";
                break;
        }
        switch ((int)transformOfPresentCube.position.y)
        {
            case 1:
                color["white"] = "011";
                color["yellow"] = "-1";
                break;
            case 0:
                color["white"] = "-1";
                color["yellow"] = "-1";
                break;
            case -1:
                color["white"] = "-1";
                color["yellow"] = "100";
                break;
        }
        switch ((int)transformOfPresentCube.position.z)
        {
            case 1:
                color["red"] = "101";
                color["orange"] = "-1";
                break;
            case 0:
                color["red"] = "-1";
                color["orange"] = "-1";
                break;
            case -1:
                color["red"] = "-1";
                color["orange"] = "110";
                break;
        }
    }

    public void rotateX(bool isClockwise = true)
    {
        int factor = isClockwise ? 1 : -1;
        string tmp;
        foreach(DictionaryEntry c in color)
        {
            if((tmp = c.ToString()) == "-1") continue;
            
        }
    }

    public bool checkColor(string color, string plane)
    {
        return this.color[color].ToString() == plane;
    }

}

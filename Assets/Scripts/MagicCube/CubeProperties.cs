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
        bool[] blue = new bool[3] { false, false, false };
        bool[] green = new bool[3] { false, false, false };
        bool[] red = new bool[3] { false, false, false };
        bool[] orange = new bool[3] { false, false, false };
        bool[] white = new bool[3] { false, false, false };
        bool[] yellow = new bool[3] { false, false, false };

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
        左：011
        右：100
        上：101
        下：110
        */
        transformOfPresentCube = GetComponent<Transform>();
        switch ((int)transformOfPresentCube.localPosition.x)
        {
            case 1:
                blue = new bool[3] { false, false, true };    //"001";
                break;
            case 0:
                break;
            case -1:
                green = new bool[3] { false, true, false }; //"010";
                break;
        }
        switch ((int)transformOfPresentCube.localPosition.y)
        {
            case 1:
                white = new bool[3] { true, false, true }; //"101";
                break;
            case 0:
                break;
            case -1:
                yellow = new bool[3] { true, true, false }; //"110";
                break;
        }
        switch ((int)transformOfPresentCube.localPosition.z)
        {
            case 1:
                red = new bool[3] { true, false, false };//"100";
                break;
            case 0:
                break;
            case -1:
                orange = new bool[3] { false, true, true }; //"011";
                break;
        }
        color.Add("blue", blue);
        color.Add("green", green);
        color.Add("red", red);
        color.Add("orange", orange);
        color.Add("white", white);
        color.Add("yellow", yellow);
    }

    public void rotateX(bool isClockwise)
    {
        bool[] tmp;
        bool[] fac1 = { false, false, true };
        bool[] fac2 = { false, true, false };
        Hashtable newColor = new Hashtable();
        foreach(DictionaryEntry c in color)
        {          
            if (!(((bool[])c.Value)[0] | ((bool[])c.Value)[1] | ((bool[])c.Value)[2])) newColor.Add(c.Key, c.Value);
            else if (((bool[])c.Value)[0] == fac1[0] && ((bool[])c.Value)[1] == fac1[1] && ((bool[])c.Value)[2] == fac1[2]) newColor.Add(c.Key, c.Value);
            else if (((bool[])c.Value)[0] == fac2[0] && ((bool[])c.Value)[1] == fac2[1] && ((bool[])c.Value)[2] == fac2[2]) newColor.Add(c.Key, c.Value);
            else
            {
                tmp = new bool[3];
                tmp[0] = (isClockwise ^ ((bool[])c.Value)[1]) | (!(isClockwise ^ ((bool[])c.Value)[2]));
                tmp[1] = isClockwise ^ ((bool[])c.Value)[2];
                tmp[2] = !(isClockwise ^ ((bool[])c.Value)[1]);
                newColor.Add(c.Key, tmp);
            }
        }
        color = newColor;
    }

    public void rotateY(bool isClockwise)
    {
        bool[] tmp;
        bool[] fac1 = { true, false, true };
        bool[] fac2 = { true, true, false };
        Hashtable newColor = new Hashtable();
        foreach (DictionaryEntry c in color)
        {
            if (!(((bool[])c.Value)[0] | ((bool[])c.Value)[1] | ((bool[])c.Value)[2])) newColor.Add(c.Key, c.Value);
            else if (((bool[])c.Value)[0] == fac1[0] && ((bool[])c.Value)[1] == fac1[1] && ((bool[])c.Value)[2] == fac1[2]) newColor.Add(c.Key, c.Value);
            else if (((bool[])c.Value)[0] == fac2[0] && ((bool[])c.Value)[1] == fac2[1] && ((bool[])c.Value)[2] == fac2[2]) newColor.Add(c.Key, c.Value);
            else
            {
                tmp = new bool[3];
                tmp[0] = (isClockwise ^ ((bool[])c.Value)[2]) & (!(isClockwise ^ ((bool[])c.Value)[1]));
                tmp[1] = !(isClockwise ^ ((bool[])c.Value)[2]);
                tmp[2] = isClockwise ^ ((bool[])c.Value)[1];
                newColor.Add(c.Key, tmp);
            }
        }
        color = newColor;
    }

    public void rotateZ(bool isClockwise)
    {
        bool[] tmp;
        bool[] fac1 = { false, true, true };
        bool[] fac2 = { true, false, false };
        Hashtable newColor = new Hashtable();
        foreach (DictionaryEntry c in color)
        {
            if (!(((bool[])c.Value)[0] | ((bool[])c.Value)[1] | ((bool[])c.Value)[2])) newColor.Add(c.Key, c.Value);
            else if (((bool[])c.Value)[0] == fac1[0] && ((bool[])c.Value)[1] == fac1[1] && ((bool[])c.Value)[2] == fac1[2]) newColor.Add(c.Key, c.Value);
            else if (((bool[])c.Value)[0] == fac2[0] && ((bool[])c.Value)[1] == fac2[1] && ((bool[])c.Value)[2] == fac2[2]) newColor.Add(c.Key, c.Value);
            else
            {
                tmp = new bool[3];
                tmp[0] = !((bool[])c.Value)[0];
                tmp[1] = !(isClockwise ^ ((((bool[])c.Value)[0]) ^ (((bool[])c.Value)[1])));
                tmp[2] = isClockwise ^ ((((bool[])c.Value)[0]) ^ (((bool[])c.Value)[1]));
                newColor.Add(c.Key, tmp);
            }
        }
        color = newColor;
    }

    public bool checkColor(string color, string plane)
    {
        return this.color[color].ToString() == plane;
    }

    public void debugLogColor()
    {
        string tmpStr = "";
        foreach (DictionaryEntry c in color)
        {
            tmpStr = "";
            tmpStr += ((bool[])c.Value)[0] ? "1" : "0";
            tmpStr += ((bool[])c.Value)[1] ? "1" : "0";
            tmpStr += ((bool[])c.Value)[2] ? "1" : "0";
            string msg = "没有";
            if (string.Equals(tmpStr, "001")) msg = "在 前 面";
            else if (string.Equals(tmpStr, "010")) msg = "在 后 面";
            else if (string.Equals(tmpStr, "011")) msg = "在 左 面";
            else if (string.Equals(tmpStr, "100")) msg = "在 右 面";
            else if (string.Equals(tmpStr, "101")) msg = "在 上 面";
            else if (string.Equals(tmpStr, "110")) msg = "在 下 面";
            Debug.Log(c.Key + msg);
        }
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CubeProperties : MonoBehaviour, CubeMessageSys {

    public float rotateAngle = 90.0f;

    float rotateSpeed;

    Hashtable color;
    Transform transformOfPresentCube;
    float indX = 90f, indY = 90f, indZ = 90f;
    float ind_X = 90f, ind_Y = 90f, ind_Z = 90f;

    void Awake()
    {
        initSelf();
    }

    void Start()
    {
        rotateSpeed = rotateAngle * Time.deltaTime;
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

    void Update()
    {
        if (indX < 90)
        {
            transform.RotateAround(Vector3.zero, Vector3.right, rotateSpeed);
            indX += rotateSpeed;
            if (indX >= 90)
            {
                indX = 90;
                initTransform();
            }
        }
        if (indY < 90)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed);
            indY += rotateSpeed;
            if (indY >= 90)
            {
                indY = 90;
                initTransform();
            }
        }
        if (indZ < 90)
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, rotateSpeed);
            indZ += rotateSpeed;
            if (indZ >= 90)
            {
                indZ = 90;
                initTransform();
            }
        }
        if (ind_X < 90)
        {
            transform.RotateAround(Vector3.zero, Vector3.left, rotateSpeed);
            ind_X += rotateSpeed;
            if (ind_X >= 90)
            {
                ind_X = 90;
                initTransform();
            }
        }
        if (ind_Y < 90)
        {
            transform.RotateAround(Vector3.zero, Vector3.down, rotateSpeed);
            ind_Y += rotateSpeed;
            if (ind_Y >= 90)
            {
                ind_Y = 90;
                initTransform();
            }
        }
        if (ind_Z < 90)
        {
            transform.RotateAround(Vector3.zero, Vector3.back, rotateSpeed);
            ind_Z += rotateSpeed;
            if (ind_Z >= 90)
            {
                ind_Z = 90;
                initTransform();
            }
        }

    }

    void initTransform() {
        ExecuteEvents.Execute<CDMessageSys>(transform.parent.gameObject, null, (m, n) => m.isReady());
        int x = getAxis(transform.localPosition.x);
        int y = getAxis(transform.localPosition.y);
        int z = getAxis(transform.localPosition.z);
        int ex = getEuler(transform.localEulerAngles.x);
        int ey = getEuler(transform.localEulerAngles.y);
        int ez = getEuler(transform.localEulerAngles.z);
        transform.localPosition = new Vector3(x,y,z);
        transform.localEulerAngles = new Vector3(ex,ey,ez);
    }

    int getAxis(float m) {
        if (m >= 0.5) return 1;
        if (m <= -0.5) return -1;
        return 0;
    }

    int getEuler(float m)
    {
        if (m >= 45 && m < 135) return 90;
        if (m >= 135 && m < 225) return 180;
        if (m >= 225 && m < 315) return 270;
        return 0;
    }

    public void iRotateX(bool isClockwise)
    {
        indX = isClockwise ? 0 : 90;
        ind_X = isClockwise ? 90 : 0;
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

    public void iRotateY(bool isClockwise)
    {
        indY = isClockwise ? 0 : 90;
        ind_Y = isClockwise ? 90 : 0;
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

    public void iRotateZ(bool isClockwise)
    {
        indZ = isClockwise ? 0 : 90;
        ind_Z = isClockwise ? 90 : 0;
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

    public void setAnglePerSec(float angle)
    {
        rotateAngle = angle;
        rotateSpeed = rotateAngle * Time.deltaTime;
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

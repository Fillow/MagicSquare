using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class RibikCubeManager : MonoBehaviour, CDMessageSys {

    public GameObject[] cubes;

    bool isCoolDown = true;
    LayerMask defaultPlane, touchPlane;

    GameObject targetA, targetB;
    string plane;
    Ray ray;

    void Start ()
    {//Layer代号： 0 ： default   8 ： 平面信息
        defaultPlane = 1 << 0;
        touchPlane = 1 << 8;
        targetA = null;
        targetB = null;
    }
    
    void Update()
    {
        if (!isCoolDown) return;
        if (Input.touches.Length == 1)
        {
            var touchA = Input.GetTouch(0);
            switch (touchA.phase)
            {
                case TouchPhase.Began:
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit planeInfo, cubeInfo;
                    if (Physics.Raycast(ray, out planeInfo, Mathf.Infinity, touchPlane.value))
                    {
                        plane = planeInfo.transform.gameObject.GetComponent<TouchPlaneProperties>().planeName;
                    }
                    if (Physics.Raycast(ray, out cubeInfo, Mathf.Infinity, defaultPlane.value))
                    {
                        targetA = cubeInfo.transform.parent.gameObject;
                    }
                    break;

                case TouchPhase.Moved:
                    if (!targetA) return;
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    if (Physics.Raycast(ray, out planeInfo, Mathf.Infinity, touchPlane.value))
                    {
                        string curPlane = planeInfo.transform.gameObject.GetComponent<TouchPlaneProperties>().planeName;
                        if (plane == curPlane)
                        {
                            if (Physics.Raycast(ray, out cubeInfo, Mathf.Infinity, defaultPlane.value))
                            {
                                targetB = cubeInfo.transform.parent.gameObject;
                                if (targetB == targetA) return;
                                else
                                {
                                    rotate(plane, targetA.transform, targetB.transform);
                                    initInfo();
                                }
                            }
                            else
                            {
                                initInfo();
                            }
                        }
                        else
                        {
                            rotate(plane, curPlane, targetA.transform);
                            initInfo();
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    initInfo();
                    break;
            }
        }
    }

    /** 【弃用的按键检测方法】
	void Update () {
        if (!isCoolDown) return;
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1)) return;
        if (Input.GetMouseButtonDown(0))//第一点，点击下去
        {
            rayA = Camera.main.ScreenPointToRay(Input.mousePosition);   
            RaycastHit planeInfo, cubeInfo;
            if (Physics.Raycast(rayA, out planeInfo, Mathf.Infinity, touchPlane.value))
            {
                plane = planeInfo.transform.gameObject.GetComponent<TouchPlaneProperties>().planeName;
                Debug.Log(plane);
            }
            if (Physics.Raycast(rayA, out cubeInfo, Mathf.Infinity, defaultPlane.value))
            {
                clickA = cubeInfo.transform.parent.gameObject;
                Debug.Log(clickA.transform.localPosition);
            }
        }
        if (Input.GetMouseButton(0))//第一点，持续点击
        {
            if (!clickA) return;
            rayA = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit planeInfo, hitInfo;

            if (Physics.Raycast(rayA, out hitInfo, Mathf.Infinity, defaultPlane.value))
            {
                clickB = hitInfo.transform.parent.gameObject;
                if (clickB != clickA)
                {//根据平面信息来排除其中一轴，再根据方块位置信息变化排除另一轴，根据面在轴上的相对信息确定顺时针或逆时针
                 //但是这种情况并不完全，因为如果第一次点击到角块的话，点击的点移动到其他的面上，旋转信息已表达清楚，但不能通过这种方式来检测
                    switch (plane)
                    {//为了之后的自由视角作准备，固定视角用这种方法很麻烦      
                        case "Front":
                            float frontY = clickB.transform.localPosition.y - clickA.transform.localPosition.y;
                            float frontZ = clickB.transform.localPosition.z - clickA.transform.localPosition.z;
                            if (Mathf.Abs(frontY) > 0.9) rotateZ((int)clickA.transform.localPosition.z, frontY > 0 ? true : false);
                            else rotateY((int)clickA.transform.localPosition.y, frontZ > 0 ? false : true);
                            break;
                        case "Back":
                            float backY = clickB.transform.localPosition.y - clickA.transform.localPosition.y;
                            float backZ = clickB.transform.localPosition.z - clickA.transform.localPosition.z;
                            if (Mathf.Abs(backY) > 0.9) rotateZ((int)clickA.transform.localPosition.z, backY < 0 ? true : false);
                            else rotateY((int)clickA.transform.localPosition.y, backZ < 0 ? false : true);
                            break;
                        case "Right":
                            float rightX = clickB.transform.localPosition.x - clickA.transform.localPosition.x;
                            float rightY = clickB.transform.localPosition.y - clickA.transform.localPosition.y;
                            if (Mathf.Abs(rightX) > 0.9) rotateY((int)clickA.transform.localPosition.y, rightX > 0 ? true : false);
                            else rotateX((int)clickA.transform.localPosition.x, rightY > 0 ? false : true);
                            break;
                        case "Left":
                            float leftX = clickB.transform.localPosition.x - clickA.transform.localPosition.x;
                            float leftY = clickB.transform.localPosition.y - clickA.transform.localPosition.y;
                            if (Mathf.Abs(leftX) > 0.9) rotateY((int)clickA.transform.localPosition.y, leftX < 0 ? true : false);
                            else rotateX((int)clickA.transform.localPosition.x, leftY < 0 ? false : true);
                            break;
                        case "Up":
                            float upX = clickB.transform.localPosition.x - clickA.transform.localPosition.x;
                            float upZ = clickB.transform.localPosition.z - clickA.transform.localPosition.z;
                            if (Mathf.Abs(upX) > 0.9) rotateZ((int)clickA.transform.localPosition.z, upX < 0 ? true : false);
                            else rotateX((int)clickA.transform.localPosition.x, upZ < 0 ? false : true);
                            break;
                          case "Down":
                            float downX = clickB.transform.localPosition.x - clickA.transform.localPosition.x;
                            float downZ = clickB.transform.localPosition.z - clickA.transform.localPosition.z;
                            if (Mathf.Abs(downX) > 0.9) rotateZ((int)clickA.transform.localPosition.z, downX > 0 ? true : false);
                            else rotateX((int)clickA.transform.localPosition.x, downZ > 0 ? false : true);
                            break;
                    }
                    clickA = null;
                    clickB = null;
                    plane = "";
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            clickA = null;
            clickB = null;
            plane = "";
            Debug.Log("Clear");
        }
    }
    */

    void rotate(string plane1, Transform tf1, Transform tf2)
    {
        switch (plane)
        {//为了之后的自由视角作准备，固定视角用这种方法很麻烦      
            case "Front":
                float frontY = tf2.localPosition.y - tf1.localPosition.y;
                float frontZ = tf2.localPosition.z - tf1.localPosition.z;
                if (Mathf.Abs(frontY) > 0.9) rotateZ((int)tf1.localPosition.z, frontY > 0 ? true : false);
                else rotateY((int)tf1.localPosition.y, frontZ > 0 ? false : true);
                break;
            case "Back":
                float backY = tf2.localPosition.y - tf1.localPosition.y;
                float backZ = tf2.localPosition.z - tf1.localPosition.z;
                if (Mathf.Abs(backY) > 0.9) rotateZ((int)tf1.localPosition.z, backY < 0 ? true : false);
                else rotateY((int)tf1.localPosition.y, backZ < 0 ? false : true);
                break;
            case "Right":
                float rightX = tf2.localPosition.x - tf1.localPosition.x;
                float rightY = tf2.localPosition.y - tf1.localPosition.y;
                if (Mathf.Abs(rightX) > 0.9) rotateY((int)tf1.localPosition.y, rightX > 0 ? true : false);
                else rotateX((int)tf1.localPosition.x, rightY > 0 ? false : true);
                break;
            case "Left":
                float leftX = tf2.localPosition.x - tf1.localPosition.x;
                float leftY = tf2.localPosition.y - tf1.localPosition.y;
                if (Mathf.Abs(leftX) > 0.9) rotateY((int)tf1.localPosition.y, leftX < 0 ? true : false);
                else rotateX((int)tf1.localPosition.x, leftY < 0 ? false : true);
                break;
            case "Up":
                float upX = tf2.localPosition.x - tf1.localPosition.x;
                float upZ = tf2.localPosition.z - tf1.localPosition.z;
                if (Mathf.Abs(upX) > 0.9) rotateZ((int)tf1.localPosition.z, upX < 0 ? true : false);
                else rotateX((int)tf1.localPosition.x, upZ < 0 ? false : true);
                break;
            case "Down":
                float downX = tf2.localPosition.x - tf1.localPosition.x;
                float downZ = tf2.localPosition.z - tf1.localPosition.z;
                if (Mathf.Abs(downX) > 0.9) rotateZ((int)tf1.localPosition.z, downX > 0 ? true : false);
                else rotateX((int)tf1.localPosition.x, downZ > 0 ? false : true);
                break;
        }
    }

    void rotate(string plane1, string plane2, Transform tf1)
    {
        switch (plane1+plane2)
        {
            case "UpRight":
            case "RightDown":
            case "DownLeft":
            case "LeftUp":
                rotateX((int) tf1.localPosition.x,true);
                break;
            case "RightUp":
            case "DownRight":
            case "LeftDown":
            case "UpLeft":
                rotateX((int)tf1.localPosition.x, false);
                break;
            case "FrontLeft":
            case "LeftBack":
            case "BackRight":
            case "RightFront":
                rotateY((int)tf1.localPosition.y, true);
                break;
            case "FrontRight":
            case "RightBack":
            case "BackLeft":
            case "LeftFront":
                rotateY((int)tf1.localPosition.y, false);
                break;
            case "FrontUp":
            case "UpBack":
            case "BackDown":
            case "DownFront":
                rotateZ((int)tf1.localPosition.z, true);
                break;
            case "FrontDown":
            case "DownBack":
            case "BackUp":
            case "UpFront":
                rotateZ((int)tf1.localPosition.z, false);
                break;
        }
    }

    void rotateX(bool isClockwise)
    {
        int i = 0;
        isCoolDown = false;
        while (i < cubes.Length)
        {
            cubes[i].GetComponent<CubeProperties>().iRotateX(isClockwise);
            i++;
        }
    }

    void rotateY(bool isClockwise)
    {
        int i = 0;
        isCoolDown = false;
        while (i < cubes.Length)
        {
            cubes[i].GetComponent<CubeProperties>().iRotateY(isClockwise);
            i++;
        }
    }

    void rotateZ(bool isClockwise)
    {
        int i = 0;
        isCoolDown = false;
        while (i < cubes.Length)
        {
            cubes[i].GetComponent<CubeProperties>().iRotateZ(isClockwise);
            i++;
        }
    }

    void rotateX(int axisX, bool isClockwise)
    {
        int i = 0;
        isCoolDown = false;
        while (i < cubes.Length)
        {
            if (cubes[i].transform.localPosition.x == axisX) cubes[i].GetComponent<CubeProperties>().iRotateX(isClockwise);
            i++;
        }
    }

    void rotateY(int axisY, bool isClockwise)
    {
        int i = 0;
        isCoolDown = false;
        while (i < cubes.Length)
        {
            if (cubes[i].transform.localPosition.y == axisY) cubes[i].GetComponent<CubeProperties>().iRotateY(isClockwise);
            i++;
        }
    }

    void rotateZ(int axisZ, bool isClockwise)
    {
        int i = 0;
        isCoolDown = false;
        while (i < cubes.Length)
        {
            if (cubes[i].transform.localPosition.z == axisZ) cubes[i].GetComponent<CubeProperties>().iRotateZ(isClockwise);
            i++;
        }
    }

    void setSpeed(float anglePerSec)
    {
        ExecuteEvents.Execute<CubeMessageSys>(gameObject, null, (x, y) => x.setAnglePerSec(anglePerSec));
    }

    public void initInfo()
    {
        targetA = null;
        targetB = null;
        plane = "";
    }

    public void isReady()
    {
        isCoolDown = true;
    }


}

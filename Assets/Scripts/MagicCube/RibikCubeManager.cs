using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class RibikCubeManager : MonoBehaviour, CDMessageSys {

    public GameObject[] cubes;

    bool isCoolDown = true;
    LayerMask defaultPlane, touchPlane;

    GameObject clickA, clickB;
    string plane;
    Ray rayA, rayB;

    void Start ()
    {//Layer代号： 0 ： default   8 ： 平面信息
        defaultPlane = 1 << 0;
        touchPlane = 1 << 8;
    }
	
	void Update () {
        if (!isCoolDown) return;
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

    void OnGUI()
    {
        if (!isCoolDown) return;
        if(GUI.Button(new Rect(10, 10, 40, 20), "X(1)")) rotateX(1, true);
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

    void setSpeed(float speed)
    {
        ExecuteEvents.Execute<CubeMessageSys>(gameObject, null, (x, y) => x.setRotateSpeed(speed));
    }

    public void isReady()
    {
        isCoolDown = true;
    }


}

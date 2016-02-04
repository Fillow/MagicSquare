using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class RibikCubeManager : MonoBehaviour, CDMessageSys {

    public GameObject[] cubes;
    bool isCoolDown = true;

    void Start () {
    }
	
	void Update () {
        
    }

    void OnGUI()
    {
        if (!isCoolDown) return;
        if(GUI.Button(new Rect(10, 10, 40, 20), "X(1)")) rotateX(1, true);
        if (GUI.Button(new Rect(10, 30, 40, 20), "-X(1)")) rotateX(1, false);
        if (GUI.Button(new Rect(50, 10, 40, 20), "X(0)")) rotateX(0, true);
        if (GUI.Button(new Rect(50, 30, 40, 20), "-X(0)")) rotateX(0, false);
        if (GUI.Button(new Rect(90, 10, 40, 20), "X(-1)")) rotateX(-1, true);
        if (GUI.Button(new Rect(90, 30, 40, 20), "-X(-1)")) rotateX(-1, false);

        if (GUI.Button(new Rect(10, 60, 40, 20), "Y(1)")) rotateY(1, true);
        if (GUI.Button(new Rect(10, 80, 40, 20), "-Y(1)")) rotateY(1, false);
        if (GUI.Button(new Rect(50, 60, 40, 20), "Y(0)")) rotateY(0, true);
        if (GUI.Button(new Rect(50, 80, 40, 20), "-Y(0)")) rotateY(0, false);
        if (GUI.Button(new Rect(90, 60, 40, 20), "Y(-1)")) rotateY(-1, true);
        if (GUI.Button(new Rect(90, 80, 40, 20), "-Y(-1)")) rotateY(-1, false);

        if (GUI.Button(new Rect(10, 110, 40, 20), "Z(1)")) rotateZ(1, true);
        if (GUI.Button(new Rect(10, 130, 40, 20), "-Z(1)")) rotateZ(1, false);
        if (GUI.Button(new Rect(50, 110, 40, 20), "Z(0)")) rotateZ(0, true);
        if (GUI.Button(new Rect(50, 130, 40, 20), "-Z(0)")) rotateZ(0, false);
        if (GUI.Button(new Rect(90, 110, 40, 20), "Z(-1)")) rotateZ(-1, true);
        if (GUI.Button(new Rect(90, 130, 40, 20), "-Z(-1)")) rotateZ(-1, false);
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

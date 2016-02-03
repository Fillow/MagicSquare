using UnityEngine;
using System.Collections;

public class RibikCubeManager : MonoBehaviour {

    public GameObject[] cubes;

    GameObject[] activeCubes;

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnGUI()
    {
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
        while (i < cubes.Length)
        {
            if (cubes[i].transform.localPosition.y == axisY) cubes[i].GetComponent<CubeProperties>().rotateY(isClockwise);
            i++;
        }
    }

    void rotateX(int axisX, bool isClockwise)
    {
        int i = 0;
        while (i < cubes.Length)
        {
            if (cubes[i].transform.localPosition.x == axisX) cubes[i].GetComponent<CubeProperties>().rotateX(isClockwise);
            i++;
        }
    }

    void rotateZ(int axisZ, bool isClockwise)
    {
        int i = 0;
        while (i < cubes.Length)
        {
            if (cubes[i].transform.localPosition.z == axisZ) cubes[i].GetComponent<CubeProperties>().rotateZ(isClockwise);
            i++;
        }
    }
}

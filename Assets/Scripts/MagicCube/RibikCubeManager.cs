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
        if(GUI.Button(new Rect(10, 10, 70, 30), "RotateX")) cubes[0].GetComponent<CubeProperties>().rotateX(true);
        if (GUI.Button(new Rect(80, 10, 70, 30), "Rotate-X")) cubes[0].GetComponent<CubeProperties>().rotateX(false);
        if (GUI.Button(new Rect(10, 50, 70, 30), "RotateY")) cubes[0].GetComponent<CubeProperties>().rotateY(true);
        if (GUI.Button(new Rect(80, 50, 70, 30), "Rotate-Y")) cubes[0].GetComponent<CubeProperties>().rotateY(false);
        if (GUI.Button(new Rect(10, 90, 70, 30), "RotateZ")) cubes[0].GetComponent<CubeProperties>().rotateZ(true);
        if (GUI.Button(new Rect(80, 90, 70, 30), "Rotate-Z")) cubes[0].GetComponent<CubeProperties>().rotateZ(false);
        if (GUI.Button(new Rect(10, 130, 70, 30), "LogColor")) cubes[0].GetComponent<CubeProperties>().debugLogColor();
    }
}

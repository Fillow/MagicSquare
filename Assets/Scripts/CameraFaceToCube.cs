using UnityEngine;
using System.Collections;

public class CameraFaceToCube : MonoBehaviour {

    public float rotSpeed = 2.0f;

    float distance;
    float x;
    float detTime = 0;

	void Start () {
        distance = Vector3.Distance(Vector3.zero, transform.position);
        transform.LookAt(Vector3.zero);
	}

    void Update()
    {
        if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            x = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
            transform.RotateAround(Vector3.zero, Vector3.up, x);
            transform.LookAt(Vector3.zero);
        }
        if (detTime < 1) detTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if(detTime < 0.3f)
            {
                detTime = 1;
                transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
                transform.LookAt(Vector3.zero);
            }
            detTime = 0;
        }
    }

}

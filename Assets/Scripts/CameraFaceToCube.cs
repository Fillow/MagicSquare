using UnityEngine;
using System.Collections;

[System.Reflection.Obfuscation(Exclude = true)]
public class CameraFaceToCube : MonoBehaviour {

    public float rotSpeed;
    public float maxDeltaTime = 0.15f;
    public GameObject magicCube;

    bool isRot = false;
    float delTime = 1;

	void Start () {
        Application.targetFrameRate = 60;
        transform.LookAt(Vector3.zero);
	}

    void Update()
    {
        UpdateTick();
        if (delTime < maxDeltaTime) delTime += Time.deltaTime;
        if (Input.touches.Length == 1)
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    if (delTime < maxDeltaTime)
                    {
                        transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
                        transform.LookAt(Vector3.zero);
                        magicCube.GetComponent<RibikCubeManager>().initInfo();
                        delTime = 2;
                    }
                    else
                    {
                        delTime = 0;
                    }
                    break;

                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    if (delTime < 2) delTime = 0;
                    break;
            }
        }
        else if (Input.touches.Length == 2)
        {
            Touch touchA = Input.touches[0];
            Touch touchB = Input.touches[1];
            switch (touchA.phase)
            {
                case TouchPhase.Moved:
                    if (Mathf.Abs(touchB.deltaPosition.y / touchA.deltaPosition.x) <= 1) isRot = true;
                    break;

                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    isRot = false;
                    break;
            }
            switch (touchB.phase)
            {
                case TouchPhase.Moved:
                    if (Mathf.Abs(touchB.deltaPosition.y / touchB.deltaPosition.x) <= 1) isRot = true;
                    break;

                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    isRot = false;
                    break;
            }
            if (isRot)
            {
                float delX = (touchA.deltaPosition.x + touchB.deltaPosition.x) * rotSpeed * Time.deltaTime;
                transform.RotateAround(Vector3.zero, Vector3.up, delX);
                transform.LookAt(Vector3.zero);
            }
        }
    }

    /** 弃用的更新方法
    void Update()
    {
        UpdateTick();
        if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
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
    */

    

    void OnGUI()
    {
        DrawFps();
    }

    private void DrawFps()
    {
        if (mLastFps > 50)
        {
            GUI.color = new Color(0, 1, 0);
        }
        else if (mLastFps > 40)
        {
            GUI.color = new Color(1, 1, 0);
        }
        else
        {
            GUI.color = new Color(1.0f, 0, 0);
        }

        GUI.Label(new Rect(10, 10, Screen.width * 0.2f, Screen.height * 0.1f), "fps: " + mLastFps);

    }

    private long mFrameCount = 0;
    private long mLastFrameTime = 0;
    static long mLastFps = 0;
    private void UpdateTick()
    {
        if (true)
        {
            mFrameCount++;
            long nCurTime = TickToMilliSec(System.DateTime.Now.Ticks);
            if (mLastFrameTime == 0)
            {
                mLastFrameTime = TickToMilliSec(System.DateTime.Now.Ticks);
            }

            if ((nCurTime - mLastFrameTime) >= 1000)
            {
                long fps = (long)(mFrameCount * 1.0f / ((nCurTime - mLastFrameTime) / 1000.0f));

                mLastFps = fps;

                mFrameCount = 0;

                mLastFrameTime = nCurTime;
            }
        }
    }
    public static long TickToMilliSec(long tick)
    {
        return tick / (10 * 1000);
    }

}

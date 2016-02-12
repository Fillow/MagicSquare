using UnityEngine;
using System.Collections;

public class CameraFaceToCube : MonoBehaviour {

    public float rotSpeed = 2.0f;

    float x;
    float detTime = 0;

	void Start () {
        Application.targetFrameRate = 60;
        transform.LookAt(Vector3.zero);
	}

    void Update()
    {
        UpdateTick();
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

using UnityEngine;
using UnityEngine.EventSystems;

public interface CubeMessageSys :  IEventSystemHandler {

    void setAnglePerSec(float iSpeed);
	
}

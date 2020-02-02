using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlScheme : MonoBehaviour
{
    public Vector2 mouseDelta;
    public bool leftMouseButton;
    public bool rightMouseButton;
    public Transform cameraRoot;
    public Camera myCamera;

    public float upAngleLimit = 40;
    public float downAngleLimit = 65;
    public float horizontalAngleLimit = 65;
    public float cameraMoveSpeed = 3f;
    public float cameraLerpSpeed = 5f;
    public float cameraZoomSpeed = 0.5f;
    public float minFOV = 20;
    public float maxFOV = 90;
    public float scrollDelta;

    public float cameraV;
    public float cameraH;

	public bool m_cursorIsLocked = true;
    public bool lockMode = false;
    
    public GrabberAimer grabAimer;
    public Grabber grabber;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.T)) {
            lockMode = !lockMode;
        }
        if(lockMode) {
            return;
        }
        mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        leftMouseButton = Input.GetMouseButton(0);
        rightMouseButton = Input.GetMouseButton(1);
        scrollDelta = Input.mouseScrollDelta.y; 
        myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView + scrollDelta * cameraZoomSpeed * Time.deltaTime, minFOV, maxFOV);
        if(Input.GetMouseButtonDown(0)) {
            grabber.TryGrab();
        }
        else if(Input.GetMouseButtonUp(0)) {
            grabber.TryDrop();
        }
        if(!rightMouseButton) {
           MoveCamera();
        } else {
            if(grabber.grabbed && grabber.grabbed.myScrewdriverScript && grabber.grabbed.myScrewdriverScript.currentScrew) {
                grabber.grabbed.myScrewdriverScript.Turn(mouseDelta);
            } else if(grabber.grabbed){
                grabber.TryRotate(mouseDelta);
            }
        }

		InternalLockUpdate();
    }

    void MoveCamera() {
        cameraV = Mathf.Clamp(Mathf.Lerp(cameraV, cameraV - mouseDelta.y * cameraMoveSpeed, cameraLerpSpeed), -upAngleLimit, downAngleLimit);
        cameraH = Mathf.Clamp(Mathf.Lerp(cameraH, cameraH + mouseDelta.x * cameraMoveSpeed, cameraLerpSpeed), -horizontalAngleLimit, horizontalAngleLimit);
        
        cameraRoot.localEulerAngles = new Vector3(cameraV, cameraH, 0);
    }

    //controls the locking and unlocking of the mouse
	private void InternalLockUpdate()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			m_cursorIsLocked = false;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			m_cursorIsLocked = true;
		}

		if (m_cursorIsLocked)
		{
			UnlockCursor();
		}
		else if (!m_cursorIsLocked)
		{
			LockCursor();
		}
	}

	private void UnlockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void LockCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}

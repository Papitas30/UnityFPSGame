using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Camera camGun;
     float targetGun;
    public float zoomMultiplier = 2;
    public float zoomGunMultiplier;
    public float defaultFov = 90;
    public float zoomDuration = 2;
    public Transform orientation;
    public Transform moveCam;
    public bool aim;
    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        aim = false;
    }

    private void Update()
    {
        transform.position = moveCam.position;
        
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if (Input.GetMouseButton(1))
        {
            targetGun = defaultFov / zoomGunMultiplier;
            ZoomCamera(defaultFov / zoomMultiplier);
            aim = true;
            
        }
        else
        {
            targetGun = defaultFov;
            ZoomCamera(defaultFov);
            aim = false;
        }
    }
    void ZoomCamera(float target)
    {
        float angle = Mathf.Abs((defaultFov / zoomMultiplier) - defaultFov);
        float angleGun = Mathf.Abs((defaultFov / zoomGunMultiplier) - defaultFov);
        Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, target, angle / zoomDuration * Time.deltaTime);
        camGun.fieldOfView = Mathf.MoveTowards(camGun.fieldOfView, targetGun, angleGun / zoomDuration * Time.deltaTime);
    }
}
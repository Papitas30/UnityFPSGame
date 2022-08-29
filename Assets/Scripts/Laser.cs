using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserStart;

    private RaycastHit rayHit;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            LaserRay();
        }
    }
    private void LaserRay()
    {
        if (Physics.Raycast(laserStart.position, laserStart.forward, out rayHit))
        {




            Vector3 forward = laserStart.forward * 20;
            Debug.DrawRay(laserStart.position, forward, Color.green, 10);

            transform.SetPositionAndRotation(rayHit.point, Quaternion.LookRotation(rayHit.normal));

        }
    }
}
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


            transform.SetPositionAndRotation(rayHit.point, Quaternion.LookRotation(rayHit.normal));

        }
    }
}
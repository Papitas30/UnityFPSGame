using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserStart;

    private SpriteRenderer laserSprite;
    private RaycastHit rayHit;
    private void Start()
    {
        laserSprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {

            LaserRay();

        }
        else
            laserSprite.enabled = false;
    }

    private void LaserRay()
    {
        if (Physics.Raycast(laserStart.position, laserStart.forward, out rayHit))
        {
            laserSprite.enabled = true;
            transform.SetPositionAndRotation(rayHit.point + (rayHit.normal * 0.01f), Quaternion.LookRotation(rayHit.normal));
        }
        else
            laserSprite.enabled = false;
    }
}

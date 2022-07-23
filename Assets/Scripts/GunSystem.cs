using TMPro;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold, readyToShoot;
    int bulletsLeft;

    //bools 
    bool shooting, reloading;

    //Reference
    public Transform muzzleFlashPoint;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public Recoil recoil;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI TMP;


    private void Awake()
    {
        bulletsLeft = magazineSize;

    }
    private void Update()
    {
        MyInput();
        

        TMP.text = (bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {


        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();


        if (bulletsLeft == 0 && !reloading && !recoil.firing) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {

            Shoot();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            recoil.firing = false;
        }

        if (bulletsLeft == 0)
        { recoil.firing = false; }
    }


    void Shoot()
    {


        recoil.Fire();
        if (!reloading)
        {
            recoil.firing = true;
        }
        //RayCast
        if (Physics.Raycast(attackPoint.transform.position, attackPoint.transform.forward, out rayHit, range, whatIsEnemy))
        {


            
        }


        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
        Instantiate(muzzleFlash, muzzleFlashPoint.position, Quaternion.LookRotation(rayHit.normal));

        bulletsLeft--;
        readyToShoot = false;

        Invoke("ResetShot", timeBetweenShots);




    }
    private void ResetShot()
    {
        readyToShoot = true;
    }



    private void Reload()
    {
        recoil.firing = false;
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}

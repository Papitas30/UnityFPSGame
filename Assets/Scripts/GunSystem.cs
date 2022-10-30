using TMPro;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float reloadTime, timeBetweenShots;
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


        if (allowButtonHold)
        {
            shooting = Input.GetMouseButton(0);
            recoil.oneTap = false;
        }
        else
        {
            shooting = Input.GetMouseButtonDown(0);
            recoil.oneTap = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();


        if (bulletsLeft == 0 && !reloading && !recoil.firing) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            recoil.firing = true;
            Shoot();

        }

        if (Input.GetMouseButtonUp(0) && !recoil.oneTap)
        {
            recoil.firing = false;
        }


    }


    void Shoot()
    {


        recoil.Fire();

        //RayCast
        if (Physics.Raycast(attackPoint.position, attackPoint.forward, out rayHit))
        {


            Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            Instantiate(muzzleFlash, muzzleFlashPoint.position, Quaternion.LookRotation(rayHit.normal));

            Vector3 forward = attackPoint.forward * 20;
            Debug.DrawRay(attackPoint.position, forward, Color.green, 10);

            if (rayHit.collider.CompareTag("Enemy"))
            {

                rayHit.collider.GetComponent<EnemyAI>().TakeDamage(damage);//Gets the EnemyAI script from the gameobject the ray hits

            }

        }

        //Graphics

        bulletsLeft--;
        readyToShoot = false;

        Invoke(nameof(ResetShot), timeBetweenShots);




    }
    private void ResetShot()
    {
        readyToShoot = true;
        recoil.firing = false;
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

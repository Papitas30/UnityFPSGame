using UnityEngine;

public class Recoil : MonoBehaviour
{
    [Header("Recoil_Transform")]

    public Transform RecoilModel;
    public Transform cameraRecoil;
    public PlayerCam script;
    public Transform GunRecoil;

    [Header("Recoil_Settings")]
    public float PositionDampTime;
    public float RotationDampTime;
    public float CameraDampTime;

    [Space(10)]
    public float Recoil1;
    public float Recoil2;
    public float Recoil3;
    public float Recoil4;

    [Space(10)]
    public Vector3 RecoilRotation;
    public Vector3 RecoilRotationAim;
    public Vector3 RecoilKickBack;
    public Vector3 RecoilKickBackAim;
    [Space(10)]

    public Vector3 CurrentRecoil1;
    public Vector3 CurrentRecoil2;
    public Vector3 CurrentRecoil3;
    public Vector3 CurrentRecoil4;

    [Space(10)]
    public Vector3 RotationOutput;
    public Vector3 RotationCam;

    public bool oneTap;
    public bool firing;

    /* Recoil1 = ROTATION
       Recoil2 = ROTATIONAim
       Recoil3 = POSITION
       Recoil4 = POSITIONAim
     */

    void Update()
    {

        CurrentRecoil1 = Vector3.Lerp(CurrentRecoil1, Vector3.zero, Recoil1 * Time.deltaTime);
        CurrentRecoil2 = Vector3.Lerp(CurrentRecoil2, Vector3.zero, Recoil2 * Time.deltaTime);
        CurrentRecoil3 = Vector3.Lerp(CurrentRecoil3, Vector3.zero, Recoil3 * Time.deltaTime);
        CurrentRecoil4 = Vector3.Lerp(CurrentRecoil4, Vector3.zero, Recoil4 * Time.deltaTime);

        if (!firing)
        {
            RotationOutput = Vector3.Slerp(RotationOutput, Vector3.zero, RotationDampTime * Time.deltaTime);
            RecoilModel.localPosition = Vector3.Slerp(RecoilModel.localPosition, Vector3.zero, PositionDampTime * Time.deltaTime);

        }
        RotationCam = Vector3.Slerp(RotationCam, RotationOutput, CameraDampTime * Time.deltaTime);
        cameraRecoil.localRotation = Quaternion.Euler(RotationCam);

        GunRecoil.localRotation = Quaternion.Euler(RotationOutput);

    }

    public void Fire()
    {

        if (script.aim)
        {

            CurrentRecoil2 += new Vector3(-RecoilRotationAim.x, Random.Range(-RecoilRotationAim.y, RecoilRotationAim.y), Random.Range(-RecoilRotationAim.z, RecoilRotationAim.z));
            CurrentRecoil4 += new Vector3(Random.Range(-RecoilKickBackAim.x, RecoilKickBackAim.x), RecoilKickBackAim.y, RecoilKickBackAim.z);

            RecoilModel.localPosition = Vector3.Slerp(RecoilModel.localPosition, CurrentRecoil4, Recoil4 * Time.deltaTime);
            RotationOutput = Vector3.Slerp(RotationOutput, CurrentRecoil2, RotationDampTime * Time.deltaTime);
            CurrentRecoil1 = CurrentRecoil2;

        }
        else if (!script.aim)
        {
            if (!oneTap)
            {
                CurrentRecoil1 += new Vector3(Random.Range(-RecoilRotation.x, RecoilRotation.x), Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
            }
            else
            {
                CurrentRecoil1 += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
            }
            CurrentRecoil3 += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), RecoilKickBack.y, RecoilKickBack.z);
            RecoilModel.localPosition += Vector3.Slerp(RecoilModel.localPosition, CurrentRecoil3, Recoil3 * Time.deltaTime);
            RotationOutput = Vector3.Slerp(RotationOutput, CurrentRecoil1, RotationDampTime * Time.deltaTime);
            CurrentRecoil2 = CurrentRecoil1;

        }

    }
}
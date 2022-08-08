using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Gun/Gun")]
public class Gun : ScriptableObject
{
    public string gunName;
    public int damage;
    public float recoilX;
    public float recoilY;
    public int range;
    public int fireRate;
    public GameObject gunModel;
}
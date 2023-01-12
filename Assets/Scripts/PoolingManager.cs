using UnityEngine;
using UnityEngine.Pool;

public class PoolingManager : MonoBehaviour
{
    //References
    [SerializeField] private GunSystem gS;
    
    [SerializeField] private DecalDestroyer flashPrefab;

    [HideInInspector] public IObjectPool<DecalDestroyer> flashPool;
    
   


}

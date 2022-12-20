using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class DecalDestroyer : MonoBehaviour
{

    public float lifeTime;
    private IObjectPool<DecalDestroyer> dPool;
    private void OnEnable()
    {
        StartCoroutine(Pooling());
    }
    private IEnumerator Pooling()
    {
        yield return new WaitForSeconds(lifeTime);
        dPool?.Release(this);
    }

    public void SetPool(IObjectPool<DecalDestroyer> pool)
    {
        dPool = pool;
    }

}

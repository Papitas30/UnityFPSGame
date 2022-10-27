using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage;
    public float animTime;
    public bool canAttack;
    private Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        { coll.enabled = true; }
    }

    private void OnTriggerEnter(Collider other)
    {
        canAttack = false;
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAI>().TakeDamage(damage);







        }

    }
}
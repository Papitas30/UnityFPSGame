using UnityEngine;

public class Knife : MonoBehaviour
{
    public int damage;
    public float animTime;
    public bool canAttack;
    private bool quickKnife;
    private bool holdKnife;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canAttack)
        {
            Debug.Log("hold");
            holdKnife = true;
            quickKnife = false;

        }
       else if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Debug.Log("quick");
            quickKnife = true;
            holdKnife = false;
           
        }

      

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (quickKnife)
            {
                Debug.Log("quick enem");

                other.GetComponent<EnemyAI>().TakeDamage(damage);
                canAttack = true;

            }
            else if (holdKnife)
            {
                Debug.Log("hold enem");
                other.GetComponent<EnemyAI>().TakeDamage(damage);
                canAttack = true;

            }
        }
    }
}
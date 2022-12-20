using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject object01;
    public GameObject object02;
    public GameObject object03;
    public GameObject object04;

    void Start()
    {
        object01.SetActive(false);
        object02.SetActive(false);
        object03.SetActive(false);
        object04.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            object01.SetActive(true);
            object02.SetActive(false);
            object03.SetActive(false);
            object04.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            object01.SetActive(false);
            object02.SetActive(true);
            object03.SetActive(false);
            object04.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            object01.SetActive(false);
            object02.SetActive(false);
            object03.SetActive(true);
            object04.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            object01.SetActive(false);
            object02.SetActive(false);
            object03.SetActive(false);
            object04.SetActive(true);

        }

    }
}
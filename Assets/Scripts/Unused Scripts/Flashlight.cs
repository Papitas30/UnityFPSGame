using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    public Light light;
    public TMP_Text text;

    
    private bool on;
    private bool off;


    void Start()
    {
        light = GetComponent<Light>();

        off = true;
        light.enabled = false;

    }



    void Update()
    {
        text.text = "100";
        

        if(Input.GetButtonDown("flashlight") && off)
        {
            
            light.enabled = true;
            on = true;
            off = false;
        }

        else if (Input.GetButtonDown("flashlight") && on)
        {
            
            light.enabled = false;
            on = false;
            off = true;
        }

       

    }

}
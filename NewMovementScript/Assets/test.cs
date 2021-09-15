using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private bool keydown = false;
    private bool keyup = false;
    private bool key = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keydown) 
        {
            Debug.Log("Pressed");
        }
        if (key) 
        {
            Debug.Log("Held");
        }
        if (keyup) 
        {
            Debug.Log("Released");
        }
    }

    public void keyUp() 
    {
        key = false;
        keyup = true;
        Invoke("keydownstop", 0.000000001f);
    }
    public void keyDown() 
    {
        key = true;
        keydown = true;
        Invoke("keydownstop", 0.000000001f);
    }

    public void keydownstop() 
    {
        keydown = false;
        keyup = false;
    }
}

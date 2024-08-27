using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerPlayer : MonoBehaviour
{
    public GameObject inventory;
    public Animator anim;
    bool SetInventoryOut;
    
    void Start()
    {
        anim.SetBool("Out", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) SetInventoryOut = !SetInventoryOut;

        if(SetInventoryOut) anim.SetBool("Out", true);
        else anim.SetBool("Out", false);
    }
}

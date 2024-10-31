using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerPlayer : MonoBehaviour
{
    public GameObject inventory;
    public Animator anim;
    public static bool SetInventoryOut;

    void Start()
    {
        anim.SetBool("Out", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab)) SetInventoryOut = true;
        else SetInventoryOut = false;

        if (SetInventoryOut) anim.SetBool("Out", false);
        else anim.SetBool("Out", true);
    }
}

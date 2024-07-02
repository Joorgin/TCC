using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    public Animator anim;
    bool setanimTimer;
    public GameObject Menu;
    float Timer;

    // Update is called once per frame
    void Update()
    {
       if(Input.anyKey)
        {
            anim.SetBool("open", true);
            setanimTimer = true;
        }

       if(setanimTimer)
        {
            Timer += Time.deltaTime;
            if(Timer >= 1)
            {
                Menu.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancas : MonoBehaviour
{
    public Animator anim;

    public void AbaixarLancas()
    {

        anim.SetBool("Levantar", false);
        Player_Mal.LevantouLanca = false;
        Debug.Log("Levantar :" + gameObject.name);
    }
}

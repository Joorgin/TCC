using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSet : MonoBehaviour
{
    public GameObject trap;
    public Transform LeftTrap;
    public Transform RightTrap;
    public static float CooldownFortrap = 10f;
    bool canTrap = true;

    [Space]
    [Header("Animator das UI Habilidades")]
    public Animator anim;
    public static float CoolDownAnimationMultiplier = 1;

    private void Update()
    {
        if (PlayerHealth.Instance.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(SetTrapper());
            }
        }
    }

    public IEnumerator SetTrapper()
    {
        if (PlayerMovement.verticalMove > 0 && canTrap)
        {
            Debug.Log(PlayerMovement.verticalMove);
            Instantiate(trap, RightTrap.transform.position, Quaternion.identity);
            canTrap = false;
        }
        if (PlayerMovement.verticalMove < 0 && canTrap)
        {
            Debug.Log(PlayerMovement.verticalMove);
            Instantiate(trap, LeftTrap.transform.position, Quaternion.identity);
            canTrap = false;
        }

        anim.SetFloat("SpeedAnimation", CoolDownAnimationMultiplier);
        anim.SetBool("HasLeftTrap", true);
        yield return new WaitForSeconds(CooldownFortrap);
        anim.SetBool("HasLeftTrap", false);
        canTrap = true;
    }

    public static void SetHabilitStatus()
    {
        float cooldownPercent = (CooldownFortrap / 100) * 10;
        if(CooldownFortrap > 5) CooldownFortrap -= cooldownPercent;

        CoolDownAnimationMultiplier += 0.1f;

        Debug.Log("Time da trap: " + CooldownFortrap);
    }
}

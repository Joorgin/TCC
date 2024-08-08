using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSet : MonoBehaviour
{
    public GameObject trap;
    public Transform LeftTrap;
    public Transform RightTrap;
    public static float CooldownFortrap = 10f;

    private void Update()
    {
        if (PlayerHealth.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(SetTrapper());
            }
        }
    }

    public IEnumerator SetTrapper()
    {
        if (PlayerMovement.verticalMove > 0)
        {
            Debug.Log(PlayerMovement.verticalMove);
            Instantiate(trap, RightTrap.transform.position, Quaternion.identity);
        }
        if (PlayerMovement.verticalMove < 0)
        {
            Debug.Log(PlayerMovement.verticalMove);
            Instantiate(trap, LeftTrap.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(CooldownFortrap);
    }

    public static void SetHabilitStatus()
    {
        float cooldownPercent = (CooldownFortrap / 100) * 10;
        if(CooldownFortrap > 5) CooldownFortrap -= cooldownPercent;
    }
}

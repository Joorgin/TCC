using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downdash : MonoBehaviour
{

    //Player RigidBody
    public Rigidbody2D playerRigidBody;
    public float dashPower;
    public static bool _isDownDash;
    public static bool CanDownDash;
    public static bool HitGround;
    public GameObject DamageArea;

    void Start()
    {
        CanDownDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) && !PlayerMovement.isGrounded && CanDownDash && PlayerHealth.isAlive) StartCoroutine(DashDown());

        if(HitGround) StartCoroutine(HitedGround());
    }

    public IEnumerator DashDown()
    {
        EnemyDamage.canTouchPlayer = false;
        playerRigidBody.velocity = Vector2.zero;
        CanDownDash = false;
        yield return new WaitForSeconds(0.6f);
        _isDownDash = true;
        playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y * dashPower);
    }

    public static void Damage()
    {
        HitGround = true;
    }

    IEnumerator HitedGround()
    {
        DamageArea.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        HitGround = false;
        DamageArea.SetActive(false);
        EnemyDamage.canTouchPlayer = true;
    }
}

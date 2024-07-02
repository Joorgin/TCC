using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public int trapCooldownAfterClick;
    public float trapCooldownDestroy;

    public GameObject enemyName;

    public Animator anim;
    bool armed;


    private void Update()
    {
        trapCooldownDestroy -= Time.deltaTime;

        if(trapCooldownDestroy <=0)
        {
            Destroy(gameObject);    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBoby") && !armed)
        {
            enemyName = collision.gameObject;
            Debug.Log(enemyName);
            StartCoroutine(Trapped());
        }
    }

    public IEnumerator Trapped()
    {
        armed = true;
        anim.SetBool("Armed", true);
        yield return new WaitForSeconds(trapCooldownAfterClick);
        Destroy(gameObject);
    }
}

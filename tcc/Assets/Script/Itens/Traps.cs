using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public int trapCooldownAfterClick;
    public float trapCooldownDestroy;

    public GameObject enemyName;

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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            enemyName = collision.gameObject;
            StartCoroutine(Trapped());
        }
    }

    public IEnumerator Trapped()
    {
        enemyName.GetComponent<ENemyBasicMovement>().isTrapped = true;
        yield return new WaitForSeconds(trapCooldownAfterClick);
        enemyName.GetComponent<ENemyBasicMovement>().isTrapped = false;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp Create(Vector3 position, int DamageAmount)
    {
        Transform damagePopUpTransform = Instantiate(GameManager.instance.pfDamagePopUp, position, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.SetUp(DamageAmount);

        return damagePopUp;
    }


    private TextMeshPro textMesh;
    private float DisappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void SetUp(int DamageAmount)
    {
        textMesh.SetText(DamageAmount.ToString());
        textColor = textMesh.color;
        DisappearTimer = 1f;
    }

    private void Update()
    {
        float MoveYSpeed = 3f;
        transform.position += new Vector3(0, MoveYSpeed) * Time.deltaTime;

        DisappearTimer -= Time.deltaTime;

        if(DisappearTimer <0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0) Destroy(gameObject);
        }
    }
}

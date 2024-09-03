using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleashMaterial : MonoBehaviour
{
    [Header("Material para trocar durante o flash")]
    [SerializeField] private Material flashMaterial;

    [Header("DuracaoDoFlash")]
    [SerializeField] private float duration;

    // o sprite que vai dar flash
    private SpriteRenderer spriteRenderer;

    // o material que estava sendo usado quando o script começou
    private Material Originalmaterial;

    // a corrotina atual
    private Coroutine flashRoutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Originalmaterial = spriteRenderer.material;
    }


    public void Flash()
    {
        if(flashRoutine != null) StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine());
    }


    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = Originalmaterial;

        flashRoutine = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunction : MonoBehaviour
{
    public void SetGameObjectOff()
    {
        gameObject.SetActive(false);
    }

    public void SetGameObjectOn(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}

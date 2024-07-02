using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleButton : MonoBehaviour
{
    public Image Setas;
    public string nameButton;
    string gameObjectName;


    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            GameObject gameObjectUnderMouse = null;

            // Check each hit to see if it's a UI element
            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject.GetComponent<Button>() != null)
                {
                    gameObjectUnderMouse = result.gameObject;
                    break;
                }
            }

            Debug.Log(gameObjectUnderMouse);

            if (gameObjectUnderMouse != null)
            {
                if (gameObjectUnderMouse.name == nameButton)
                {
                    Setas.gameObject.SetActive(true);
                }
                else
                {
                    Setas.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Setas.gameObject.SetActive(false);
        }
    }
}

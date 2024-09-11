using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite Icon;
    //public Sprite Icon2;
}

[System.Serializable]
public class DialogLine
{
    public DialogueCharacter character;
    [TextArea (3,10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{ 
   public List<DialogLine> dialogLines = new List<DialogLine>();
}


public class Dialog : MonoBehaviour
{
    public Dialogue Dialogue;
    //public Dialogue Dialogue2;
    public static bool inRange;
    public static bool ReadyToTalk;
    public static bool isInDialog;
    public static bool hasStartTalking;
    public static int levelOfHealthTotem;
    public string Name;
    string gameObjectName;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && inRange && !isInDialog && gameObjectName == Name)
        {
            ReadyToTalk = true;
            isInDialog = true;
            TriggerDialogue();
            StartCoroutine(hasStartedTalking());
        }

        if (Input.GetKey(KeyCode.Space) && inRange && isInDialog && hasStartTalking)
        {
            StartCoroutine(SpeedTalk());
            Interação.buttonOff = true;
        }
    }

    public IEnumerator hasStartedTalking()
    {
        yield return new WaitForSeconds(2);
        hasStartTalking = true;
    }

    public IEnumerator SpeedTalk()
    {
        DialogManager.typingSpeed = 0f;
        yield return new WaitForSeconds(1);
        DialogManager.typingSpeed = 0.2f;
    }

    public void TriggerDialogue()
    {
        DialogManager.instance.StartDialog(Dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inRange = true;
            gameObjectName = gameObject.name;
            Debug.Log("Name : " + collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = false;
            gameObjectName = null;
            hasStartTalking = false;
        }
    }
}

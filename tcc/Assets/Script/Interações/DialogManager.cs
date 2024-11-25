using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialoguearea;

    private Queue<DialogLine> Lines;

    public bool isDialogActive = false;
    public static float typingSpeed = 0.2f;
    public Animator anim;

    void Start()
    {
        if (instance == null)
            instance = this;

        Lines = new Queue<DialogLine>();  
    }

    public void StartDialog(Dialogue dialogue)
    {
        isDialogActive = true;

        anim.Play("Show");

        Lines.Clear();

        foreach(DialogLine dialogLine in dialogue.dialogLines)
        {
            Lines.Enqueue(dialogLine);
        }

        DisplayNextDialogueLine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) DisplayNextDialogueLine();
    }
    public void DisplayNextDialogueLine()
    {
        if (!Dialog.ReadyToTalk) return;
        if (Lines.Count == 0) 
        {
            EndDialog();
            return;
        }

        DialogLine currentLine = Lines.Dequeue();

        characterIcon.sprite = currentLine.character.Icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogLine dialogLine)
    {
        dialoguearea.text = "";
        foreach (char letter in dialogLine.line.ToCharArray())
        {
            dialoguearea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialog()
    {
        isDialogActive = false;
        anim.Play("Hide");
        Dialog.ReadyToTalk = false;
        Dialog.hasStartTalking = false;
        Interação.buttonOff = false;
        GameManager.instance.isInConversation = false;
        StartCoroutine(EndOfDialog());
    }

    IEnumerator EndOfDialog()
    {
        yield return new WaitForSeconds(1f);
        Dialog.isInDialog = false;
    }
}

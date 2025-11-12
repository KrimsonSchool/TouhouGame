using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image portrait1;
    public Image portrait2;
    public TextMeshProUGUI dialogueText;

    public Dialogue currentDialogue;

    public Dialogue[] dialogues;
    int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(updateText());
        index = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (dialogueText.text == currentDialogue.text)
            {
                index++;
                currentDialogue = dialogues[index];
                UpdateSpeaker();
                StartCoroutine(updateText());
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = currentDialogue.text;
            }
        }
    }

    public IEnumerator updateText()
    {
        dialogueText.text = "";
        char[] txt = currentDialogue.text.ToCharArray();
        for (int i = 0; i < txt.Length; i++)
        {
            dialogueText.text += txt[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void UpdateSpeaker()
    {
        portrait1.sprite = currentDialogue.talker1;
        portrait2.sprite = currentDialogue.talker2;
    }
}

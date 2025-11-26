using System.Collections;
using UnityEngine;

public class DialogueConversation : MonoBehaviour
{
    public int bossIndex;
    public Dialogue currentDialogue;
    public Dialogue[] dialogues;
    int index;
    
    DialogueManager dialogueManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        index = -1;
        UpdateSpeaker();
        StartCoroutine(updateText());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (dialogueManager.dialogueText.text == currentDialogue.text)
            {
                index++;
                if (index > dialogues.Length-1)
                {
                    gameObject.SetActive(false);
                    dialogueManager.EndConversation(bossIndex);
                    FindFirstObjectByType<WaveManager>().SpawnBoss(bossIndex);
                    return;
                }
                currentDialogue = dialogues[index];
                UpdateSpeaker();
                StartCoroutine(updateText());
            }
            else
            {
                StopAllCoroutines();
                dialogueManager.dialogueText.text = currentDialogue.text;
            }
        }
    }
    
    public IEnumerator updateText()
    {
        dialogueManager.dialogueText.text = "";
        char[] txt = currentDialogue.text.ToCharArray();
        for (int i = 0; i < txt.Length; i++)
        {
            dialogueManager.dialogueText.text += txt[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void UpdateSpeaker()
    {
        dialogueManager.portrait1.sprite = currentDialogue.talker1;
        dialogueManager.portrait2.sprite = currentDialogue.talker2;
    }
}

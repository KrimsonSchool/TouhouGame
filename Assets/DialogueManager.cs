using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image portrait1;
    public Image portrait2;
    public TextMeshProUGUI dialogueText;

    public GameObject[] conversations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversation(int no)
    {
        GetComponent<Animator>().SetBool("Dialogue", true);
        conversations[no].SetActive(true);
    }

    public void EndConversation(int no)
    {
        GetComponent<Animator>().SetBool("Dialogue", false);
        conversations[no].SetActive(false);
    }
}

using UnityEngine;
using TMPro;

public class Dia : MonoBehaviour
{
    public TextMeshPro[] dialogueTexts; 
    public string[] dialogues; 

    private int currentDialogueIndex = 0; 
    private bool dialogueInProgress = false;  

    public Transform controller;  
    public float triggerDistance = 57.0f; 

    private bool isTriggered = false;  

    private void Start()
    {
        Debug.Log("Script started. Hiding all dialogues.");
        HideAllDialogues(); 
    }

    private void Update()
    {
        if (controller == null)
        {
            Debug.LogError("Controller is not bound, please check.");
            return;
        }

     
        float distance = Vector3.Distance(controller.position, transform.position);
        Debug.Log("Current Distance: " + distance);  

        
        if (distance <= triggerDistance && !isTriggered)
        {
            Debug.Log("Player entered the trigger zone.");
            StartDialogue();
            isTriggered = true;  
        }

       
        if (dialogueInProgress && OVRInput.GetDown(OVRInput.Button.One))  
        {
            Debug.Log("A button pressed. Showing next dialogue.");
            ShowNextDialogue();
        }
    }

    private void StartDialogue()
    {
        dialogueInProgress = true;
        currentDialogueIndex = 0;  
        Debug.Log("Starting dialogue at index: " + currentDialogueIndex);
        ShowDialogueAt(currentDialogueIndex);  
    }

    private void ShowNextDialogue()
    {
        currentDialogueIndex++; 

        if (currentDialogueIndex < dialogues.Length)
        {
            Debug.Log("Showing next dialogue at index: " + currentDialogueIndex);
            ShowDialogueAt(currentDialogueIndex);
        }
        else
        {
            Debug.Log("Dialogue ended.");
            EndDialogue();  
        }
    }

    private void ShowDialogueAt(int index)
    {
        HideAllDialogues(); 
        dialogueTexts[index].text = dialogues[index]; 
        dialogueTexts[index].gameObject.SetActive(true);  
        Debug.Log("Dialogue shown: " + dialogues[index]);
    }

    private void HideAllDialogues()
    {
        Debug.Log("Hiding all dialogues.");
        foreach (var text in dialogueTexts)
        {
            text.text = ""; 
            text.gameObject.SetActive(false);  
        }
    }

    private void EndDialogue()
    {
        HideAllDialogues();  
        dialogueInProgress = false;  
        isTriggered = false; 
    }
}

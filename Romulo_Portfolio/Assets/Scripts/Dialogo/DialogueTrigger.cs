using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;                      //variavel que contem todas as informações do dialogo
    
    //procura pela instância da class DialogueManager e trigga um diálogo lá
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}

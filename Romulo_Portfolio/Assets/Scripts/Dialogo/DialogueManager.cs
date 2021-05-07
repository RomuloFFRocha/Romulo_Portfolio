using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Text nameText;                             //componente Text do nome
    [SerializeField] Text dialogueText;                         //componente Text da sentença

    [Header("Objetos")]
    [SerializeField] GameObject nameTextObject;                 //objeto que possuí o componente Text do nome
    [SerializeField] GameObject dialogueTextObject;             //objeto que possuí o componente Text do sentença
    [SerializeField] GameObject continueButtonObject;           //botão para passar para o proximo dialogo
    [SerializeField] GameObject dialogueBoxObject;              //caixa de dialogo

    Queue<string> names = new Queue<string>();                  //fila de nomes para serem colcoados na tela
    Queue<string> sentences = new Queue<string>();              //fila de sentenças para serem colcoados na tela

    UnityEvent eventToTrigger;

    public static DialogueManager instance;

    //checa se o dialogue manager já está instânciado na cena, e destrói uma possível segunda instância
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    //método chamado para receber todas as variaveis presentes na class Dialogue, ligar os componentes de UI e iniciar o dialogo
    public void StartDialogue(Dialogue dialogue)
    {
        nameTextObject.SetActive(true);
        dialogueTextObject.SetActive(true);
        continueButtonObject.SetActive(true);
        dialogueBoxObject.SetActive(true);

        sentences.Clear();

        foreach (Sentence sentence in dialogue.sentences)
        {
            names.Enqueue(sentence.name);
            sentences.Enqueue(sentence.sentence);
        }

        eventToTrigger = dialogue.eventToTrigger;

        DisplayNextSentence();
    }

    //checa se a fila de diálogo não está vazia e monta a proxima linha de dialogo na UI
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (eventToTrigger != null)
                eventToTrigger.Invoke();

            EndDialogue();

            return;
        }

        string name = names.Dequeue();

        nameText.text = name;

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
    }

    //desativa os componentes da UI
    void EndDialogue()
    {
        nameTextObject.SetActive(false);
        dialogueTextObject.SetActive(false);
        continueButtonObject.SetActive(false);
        dialogueBoxObject.SetActive(false);
    }
}

using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public Sentence[] sentences;                         //conjunto de sentence(compostas por nome e fala dos personagens)
    public UnityEvent eventToTrigger;                    //evento que pode ser triggado ao final da sessão de diálogo
}

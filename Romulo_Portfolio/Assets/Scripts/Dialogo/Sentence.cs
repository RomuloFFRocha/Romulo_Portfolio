using UnityEngine;

[System.Serializable]
public class Sentence
{
    public string name;                         //nome do personagem

    [TextArea(3, 10)]
    public string sentence;                     //fala da personagem
}

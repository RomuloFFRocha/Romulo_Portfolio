using UnityEngine;

[CreateAssetMenu(menuName = ("Text Presets"))]
public class TextPresets : ScriptableObject
{
    public FontStyle font;

    public int fontSize;

    public Color fontColor;

    public TextAnchor textAnchor;
}

using UnityEngine;

[CreateAssetMenu(menuName =("Outline Presets"))]
public class OutlinePresets : ScriptableObject
{
    public Color outlineColor = Color.white;

    [Range(0, 10)]
    public float outlineWidth = 6;

    [Space]
    public float maxDistance = 2;

}

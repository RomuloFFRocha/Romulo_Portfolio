using UnityEngine;

[CreateAssetMenu(menuName = ("Pressure Door Puzzle"))]
public class PressureDoorPuzzle : ScriptableObject
{
    public int initialPressureValue;
    public int finalPressureValue;
    public int maxActivationTimes;
}

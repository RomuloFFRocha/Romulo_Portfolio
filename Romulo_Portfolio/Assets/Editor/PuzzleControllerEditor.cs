using UnityEditor;

[CustomEditor(typeof(PuzzleController)), CanEditMultipleObjects]
public class PuzzleControllerEditor : Editor
{
    public SerializedProperty
        puzzles_Prop,
        eventToTrigger_Prop;

    public void OnEnable()
    {
        puzzles_Prop = serializedObject.FindProperty("puzzles");
        eventToTrigger_Prop = serializedObject.FindProperty("eventToTrigger");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PuzzleController myPuzzleController = (PuzzleController)target;

        EditorGUILayout.PropertyField(puzzles_Prop);

        EditorGUILayout.PropertyField(eventToTrigger_Prop);

        serializedObject.ApplyModifiedProperties();
    }
}

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(SelectionController)), CanEditMultipleObjects]
public class SelectionControllerEditor : Editor
{
    public SerializedProperty
        selectableObjects_Prop,
        pickUpObjects_Prop,
        rightHandObjects_Prop,
        neededTool_Prop,
        maskSounds_Prop,
        eventToTrigger_Prop;

    public void OnEnable()
    {
        selectableObjects_Prop = serializedObject.FindProperty("selectableObjects");
        pickUpObjects_Prop = serializedObject.FindProperty("pickUpObjects");
        rightHandObjects_Prop = serializedObject.FindProperty("rightHandObjects");
        neededTool_Prop = serializedObject.FindProperty("neededTool");
        maskSounds_Prop = serializedObject.FindProperty("maskSounds");
        eventToTrigger_Prop = serializedObject.FindProperty("eventToTrigger");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SelectionController mySelectionController = (SelectionController)target;

        EditorGUILayout.PropertyField(selectableObjects_Prop);

        SelectionController.SelectableObjects sObj = (SelectionController.SelectableObjects)selectableObjects_Prop.enumValueIndex;

        switch (sObj)
        {
            case SelectionController.SelectableObjects.pickUpObject:

                EditorGUILayout.PropertyField(pickUpObjects_Prop);

                SelectionController.PickUpObjects pUO = (SelectionController.PickUpObjects)pickUpObjects_Prop.enumValueIndex;

                switch (pUO)
                {
                    case SelectionController.PickUpObjects.rightHand:

                        EditorGUILayout.PropertyField(rightHandObjects_Prop);

                        SelectionController.RightHandObjects rHO = (SelectionController.RightHandObjects)rightHandObjects_Prop.enumValueIndex;
                        
                        mySelectionController.rightHandConteiner = (Transform)EditorGUILayout.ObjectField("Right Hand Conteiner", mySelectionController.rightHandConteiner, typeof(Transform), true);

                        mySelectionController.correspondingObject = (GameObject)EditorGUILayout.ObjectField("Corresponding Object", mySelectionController.correspondingObject, typeof(GameObject), true);

                        break;

                    case SelectionController.PickUpObjects.leftHand:

                        mySelectionController.leftHandConteiner = (Transform)EditorGUILayout.ObjectField("Left Hand Conteiner", mySelectionController.leftHandConteiner, typeof(Transform), true);

                        mySelectionController.correspondingObject = (GameObject)EditorGUILayout.ObjectField("Corresponding Object", mySelectionController.correspondingObject, typeof(GameObject), true);

                        break;
                }

                break;

            case SelectionController.SelectableObjects.openObject:
                
                break;

            case SelectionController.SelectableObjects.readObject:

                mySelectionController.sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", mySelectionController.sprite, typeof(Sprite), false);

                mySelectionController.showUpImage = (Image)EditorGUILayout.ObjectField("Show Up Image", mySelectionController.showUpImage, typeof(Image), true);
                mySelectionController.showUpImageObject = (GameObject)EditorGUILayout.ObjectField("Show Up Image Object", mySelectionController.showUpImageObject, typeof(GameObject), true);

                break;

            case SelectionController.SelectableObjects.puzzleObject:

                EditorGUILayout.PropertyField(neededTool_Prop);

                SelectionController.NeededTool nT = (SelectionController.NeededTool)neededTool_Prop.enumValueIndex;

                break;

            case SelectionController.SelectableObjects.interactableObject:

                EditorGUILayout.PropertyField(eventToTrigger_Prop);

                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

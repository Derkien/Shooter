using Shooter.Helper;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Shooter.Editor
{

    [CustomEditor(typeof(ObjectGenerator))]
    public class ObjectGeneratorEditor : UnityEditor.Editor
    {
        private int selectedToSave = 0;
        private int selectedToLoad = 0;
        private int selectedToDelete = 0;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ObjectGenerator myScript = (ObjectGenerator)target;
            if (GUILayout.Button("Generate Objects On Map"))
            {
                myScript.GenerateObjects();

                return;
            }

            var gameObjects = myScript.FindAllGeneratedObjects();

            if (gameObjects.Length > 0)
            {
                List<string> options = new List<string>();

                for (int i = 0; i < gameObjects.Length; i++)
                {
                    options.Add(gameObjects[i].name);
                }

                EditorGUILayout.Space();
                selectedToSave = EditorGUILayout.Popup("Group to save", selectedToSave, options.ToArray());

                if (GUILayout.Button("Save selected"))
                {
                    myScript.SaveGenerated(options[selectedToSave]);

                    return;
                }

                EditorGUILayout.Space();
                selectedToDelete = EditorGUILayout.Popup("Group to delete", selectedToDelete, options.ToArray());
                if (gameObjects.Length > 0 && GUILayout.Button("Delete selected"))
                {
                    myScript.DeleteGenerated(options[selectedToSave]);

                    selectedToSave = 0;
                    selectedToDelete = 0;

                    return;
                }

            }

            if (myScript.IsSavedDataExists())
            {
                var options = myScript.GetSavedFileNameArray();

                EditorGUILayout.Space();
                selectedToLoad = EditorGUILayout.Popup("Group to load", selectedToLoad, options);
                if (GUILayout.Button("Load selected"))
                {
                    myScript.LoadGenerated(options[selectedToLoad]);

                    return;
                }
            }
        }
    }
}

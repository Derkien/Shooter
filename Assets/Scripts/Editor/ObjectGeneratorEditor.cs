using UnityEditor;
using UnityEngine;

namespace Shooter
{
    [CustomEditor(typeof(ObjectGenerator))]
    public class ObjectGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ObjectGenerator myScript = (ObjectGenerator)target;
            if (GUILayout.Button("Generate Objects On Map"))
            {
                myScript.GenerateObjects();
            }
        }
    }
}

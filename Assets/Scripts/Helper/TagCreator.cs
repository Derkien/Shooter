using UnityEditor;

namespace Shooter.Helper
{
    public class TagCreator
    {
        public static bool CreateTag(string tagName)
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty tagsProp = tagManager.FindProperty("tags");

            bool found = false;

            for (int i = 0; i < tagsProp.arraySize; i++)
            {
                SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
                if (t.stringValue.Equals(tagName))
                {
                    found = true;
                    return true;
                }
            }

            if (!found)
            {
                tagsProp.InsertArrayElementAtIndex(0);
                SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
                n.stringValue = tagName;
                tagManager.ApplyModifiedPropertiesWithoutUndo();
            }

            return true;
        }
    }
}

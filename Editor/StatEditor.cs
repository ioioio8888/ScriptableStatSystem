using UnityEngine;
using UnityEditor;
using ScriptableStatSystem;

namespace ScriptableStatSystem
{
    [CustomEditor(typeof(CharacterStatSystem))]
    public class StatEditor : Editor
    {
        bool showStats = true;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CharacterStatSystem characterStatSystem = (CharacterStatSystem)target;
            showStats = EditorGUILayout.BeginFoldoutHeaderGroup(showStats, "stats");
            if (showStats)
            {
                if (characterStatSystem.StatSystem != null)
                {
                    foreach (var stat in characterStatSystem.StatSystem.stats)
                    {
                        EditorGUILayout.LabelField(stat.Key.Name, stat.Value.value.ToString());
                    }
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }


    }
}
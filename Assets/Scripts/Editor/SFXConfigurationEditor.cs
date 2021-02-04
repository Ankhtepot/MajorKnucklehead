using ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SFXConfiguration))]
    public class SFXConfigurationEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            // DrawDefaultInspector();

            if (GUILayout.Button("PlayTest audioClip"))
            {
                (target as SFXConfiguration).PlayClip();
            }

        }
    }
}
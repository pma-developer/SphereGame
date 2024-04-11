
using UnityEngine;
using UnityEditor;
using System.IO;

namespace SphereGame
{

    [CustomEditor(typeof(GameConfig))]
    public class GameConfigEditor : Editor
    {
        private string filePathInput = "config.json";
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var gameConfig = (GameConfig) target;

            filePathInput = EditorGUILayout.TextField("File path inside Assets folder", filePathInput);
            var filePath = Path.Join(Application.dataPath, filePathInput);

            if (GUILayout.Button("Save to JSON"))
            {
                if (!string.IsNullOrEmpty(filePath) && 
                    Path.IsPathFullyQualified(filePath))
                {
                    gameConfig.SaveToJson(filePath);
                }
                else
                {
                    Debug.LogError($"Invalid path {filePath}");
                }
            }

            if (GUILayout.Button("Load from JSON"))
            {
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    gameConfig.LoadFromJson(filePath);
                    EditorUtility.SetDirty(target);
                }
                else
                {
                    Debug.LogError($"File not found at {filePath}");
                }
            }
        }

    }
}
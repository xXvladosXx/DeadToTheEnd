using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace IDZ_Digital.Scene_Switcher
{
    public class SceneSwitcherWindow : EditorWindow
    {
        private Vector2 _scrollPos;
        private bool _openSceneAdditive;

        private static List<(string, string)> _scenePaths = new List<(string, string)>();

        private static string _sceneParentLocation = "Assets";

        private const string LocationPrefKey = "IDZ_SCENE_SWITCHER_SELECTED_PATH";

        [MenuItem("Window/Scene Switcher &l")]
        public static void ShowWindow()
        {
            var window = GetWindow<SceneSwitcherWindow>("Scene Switcher");
            _sceneParentLocation = PlayerPrefs.GetString(LocationPrefKey, "Assets");
            window.Show();
            PopulateScenes();
        }

        private void OnBecameVisible()
        {
            ShowWindow();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Scene Location", GUILayout.Width(95));
            GUI.enabled = false;
            var pathStyle = new GUIStyle(GUI.skin.textField) {margin = new RectOffset(0, 10, 0, 0)};

            EditorGUILayout.TextField(_sceneParentLocation, pathStyle);
            GUI.enabled = true;
            if (GUILayout.Button("Select Location", GUILayout.Width(100)))
            {
                var selectedPath = EditorUtility.OpenFolderPanel("Select Folder for scenes", _sceneParentLocation, "");
                try
                {
                    selectedPath = selectedPath.Substring(selectedPath.LastIndexOf("Assets", StringComparison.Ordinal));
                }
                catch (Exception e)
                {
                    Debug.Log($"Please select valid path. Error: {e}");
                    return;
                }

                _sceneParentLocation = selectedPath;

                PlayerPrefs.SetString(LocationPrefKey, _sceneParentLocation);

                ShowWindow();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            _openSceneAdditive = EditorGUILayout.Toggle("Open Scene Additive", _openSceneAdditive);

            if (GUILayout.Button("Refresh", GUILayout.Width(100)))
            {
                ShowWindow();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, false, false, GUILayout.Width(230));


            foreach (var (sceneName, scenePath) in _scenePaths)
            {
                if (!GUILayout.Button(sceneName, GUILayout.Width(180f))) continue;
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) continue;
                if (_openSceneAdditive) EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                else EditorSceneManager.OpenScene(scenePath);
            }
            
            EditorGUILayout.EndScrollView();
            GUILayout.FlexibleSpace();
            var style = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 17
            };

            GUILayout.Label("IDZ Digital", style, GUILayout.ExpandWidth(true));
            EditorGUILayout.Space(8);
        }

        private static void PopulateScenes()
        {
            _scenePaths = new List<(string, string)>();
            
            var files = AssetDatabase.FindAssets("t:Scene", new[] {_sceneParentLocation});


            foreach (var t in files)
            {
                var path = AssetDatabase.GUIDToAssetPath(t);

                var sceneName = path.Substring(path.LastIndexOf('/') + 1);
                sceneName = sceneName.Replace(".unity", "");
                _scenePaths.Add((sceneName, path));
            }

            _scenePaths = _scenePaths.OrderBy(x => x.Item1).ToList();
        }
    }
}
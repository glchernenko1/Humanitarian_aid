using System;
using System.Collections;
using System.Collections.Generic;
using log4net.Core;
using UnityEditor;
using UnityEngine;
using  System.Xml;
public class LevelEditor : EditorWindow
{
    private Transform _parent;
    private EditorData _data;
    private int _index;
    private int _lvl = 0;

    private bool _isEnable;

    private GameLevel _gameLevel;
    public SceneEditor _sceneEditor;

    [MenuItem("Window/Level Editor")]
    public static void Init()
    {
        LevelEditor levelEditor = GetWindow<LevelEditor>("Level Editor");
        levelEditor.Show();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        _parent = (Transform) EditorGUILayout.ObjectField(_parent, typeof(Transform), true);
        GUILayout.Space(30);
        if (_data == null)
        {
            if (GUILayout.Button("Load Data"))
            {
                _data = (EditorData) AssetDatabase.LoadAssetAtPath("Assets/Editor/EditorData.asset",
                    typeof(EditorData));
                _sceneEditor = CreateInstance<SceneEditor>();
                _sceneEditor.SetLevelEditor(this, _parent);
            }
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Blocks", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("<", GUILayout.Width(50), GUILayout.Height(50)))
            {
                _index--;
                if (_index < 0)
                {
                    _index = _data.BlockDates.Count - 1;
                }
            }

            GUILayout.BeginVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(_data.BlockDates[_index].Texture2D);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(_data.BlockDates[_index].Name);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            if (GUILayout.Button(">", GUILayout.Width(50), GUILayout.Height(50)))
            {
                _index++;
                if (_index > _data.BlockDates.Count - 1)
                {
                    _index = 0;
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(30);
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Level", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(20)))
            {
                _lvl--;
                if (_lvl < 0)
                {
                    _lvl = 29;
                }
            }
            GUILayout.BeginVertical();
            GUILayout.Label(((int)(_lvl+1)).ToString());
            GUILayout.EndVertical();
            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)))
            {
                _lvl++;
                if (_lvl > 29)
                {
                    _lvl = 0;
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(30);

            GUI.color = _isEnable ? Color.green : Color.white;
            if (GUILayout.Button("Create blocks"))
            {
                _isEnable = !_isEnable;

                if (_isEnable)
                {
                    SceneView.duringSceneGui += _sceneEditor.OnSceneGUI;
                }
                else
                {
                    SceneView.duringSceneGui -= _sceneEditor.OnSceneGUI;
                }
            }

            GUI.color = Color.white;
            GUILayout.Space(30);
            
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Save Level"))
            {
                SaveLevel saveLevel = new SaveLevel();
                _gameLevel.Blocks = saveLevel.GetBlocks(_lvl);
                EditorUtility.SetDirty(_gameLevel);
                Debug.Log("Level Saved");
            }
            
            if(GUILayout.Button("Load Level")) LoadLvL();
              
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            if(GUILayout.Button("Clear"))
            {
                GameObject[] gb = GameObject.FindGameObjectsWithTag("Block");
                
                for(int i = 0; i < gb.Length;i++)
                {
                    DestroyImmediate(gb[i]);
                }
                
                GameObject[] gbs = GameObject.FindGameObjectsWithTag("RedBlock");
                
                for(int i = 0; i < gbs.Length;i++)
                {
                    DestroyImmediate(gbs[i]);
                }
                
                
            }
            
        }
    }
    
    public GameObject GetBlock()
    {
        return _data.BlockDates[_index].BlockData;
    }
    
    public void LoadLvL()
    {
        XmlTextReader reader = new XmlTextReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"/My Games/Arkanoid" + "//xmlLevel" + _lvl + ".xml");
        reader.WhitespaceHandling = WhitespaceHandling.None;
        Debug.Log("-------------LoadXML------------");
        string load = "";
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Text)
                load = reader.Value;
        }
        string[] np = load.Split('|');
        Debug.Log(np[0].Length);
        string[] position = np[1].Split('+');
        for (int i = 0; i < position.Length - 1; i++)
        {
            string[] vecStr = position[i].Split(':');
            Vector3 vec = new Vector3(float.Parse(vecStr[0]), float.Parse(vecStr[1]), 0);
            GameObject obj;
            switch (np[0][i])
            {
                case ('R'):
                    obj = Instantiate(_data.BlockDates[2].BlockData);
                    obj.transform.position = vec;
                    break;
                case ('G'):
                    obj = Instantiate(_data.BlockDates[1].BlockData);
                    obj.transform.position = vec;
                    break;
                case ('B'):
                    obj = Instantiate(_data.BlockDates[0].BlockData);
                    obj.transform.position = vec;
                    break;
                case ('Y'):
                    obj = Instantiate(_data.BlockDates[4].BlockData);
                    obj.transform.position = vec;
                    break;
                case ('S'):
                    obj = Instantiate(_data.BlockDates[3].BlockData);
                    obj.transform.position = vec;
                    break;
            }
        }
    }
    
}
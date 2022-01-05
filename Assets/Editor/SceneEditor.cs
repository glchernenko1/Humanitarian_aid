using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneEditor : EditorWindow
{
    // private readonly EditorGrid _grid = new EditorGrid();
    private LevelEditor _levelEditor;
    private Transform _parent;

    public void SetLevelEditor(LevelEditor levelEditor, Transform parent)
    {
        _parent = parent;
        _levelEditor = levelEditor;
    }

    public void OnSceneGUI(SceneView sceneView)
    {
        
        Event current = Event.current;
        if(current.type == EventType.MouseDown)
        {
            Vector3 point = sceneView.camera.ScreenToWorldPoint(new Vector3(current.mousePosition.x, sceneView.camera.pixelHeight - current.mousePosition.y, 1));
            Vector3 position = new Vector3(point.x, point.y, 0);
            if(position !=Vector3.zero)
            {
                if (IsEmpty(position))
                {
                    Debug.Log("+");
                    GameObject game = Instantiate(_levelEditor.GetBlock(), _parent) as GameObject;
                    game.transform.position = position;
                    
                }
            }
        }
        if(current.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
        }
    }

    private bool IsEmpty(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapCircle(position, 0.01f);
        return collider == null;
    }
}
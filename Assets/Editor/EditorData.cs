using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName ="EditorData",menuName ="EditorData/Create/Data")]
public class EditorData : ScriptableObject
{
    public List<EditorBlockData> BlockDates = new List<EditorBlockData>();
}

[System.Serializable]
public class EditorBlockData
{
    public Texture2D Texture2D;
    public GameObject BlockData;
    public string Name;
}
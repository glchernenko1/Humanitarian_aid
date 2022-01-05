using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

public class SaveLevel : MonoBehaviour
{
    public List<BlockObject> GetBlocks(int lvl)
    {
        List<BlockObject> objects = new List<BlockObject>();
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
        GameObject[] allBlocks1 = GameObject.FindGameObjectsWithTag("RedBlock");

        foreach( var item in allBlocks)
        {
            BlockObject block = new BlockObject();
            block.Position = item.gameObject.transform.position;
            block.Block = item;
            objects.Add(block);
        }
        foreach( var item in allBlocks1)
        {
            Debug.Log("++");
            BlockObject block = new BlockObject();
            block.Position = item.gameObject.transform.position;
            block.Block = item;
            objects.Add(block);
        }
        
        SaveOnXML(lvl, objects);
        return objects;
    }

    public void SaveOnXML(int lvl, List<BlockObject> blocks)
    {
        string blockName = "";
        string blockPosition = "";
        XmlSerializer writer = new XmlSerializer(typeof(string));
        for (int i = 0; i < blocks.Count; i++)
        {
            switch (blocks[i].Block.name) {
                case ("Green Block(Clone)"):
                    blockName += 'G';
                    break;
                case ("Blue Block(Clone)"):
                    blockName += 'B';
                    break;
                case ("Red Block(Clone)"):
                    blockName += 'R';
                    break;
                case ("Yellow Block(Clone)"):
                    blockName += 'Y';
                    break;
                case ("RedOorX(Clone)"):
                    blockName += 'S';
                    break;
            }
            blockPosition += blocks[i].Position.x.ToString()+":"+ blocks[i].Position.y.ToString();
            blockPosition += "+";
        }
        Debug.Log("----------------------Save------------------");
        Debug.Log(blockName);
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +"/My Games/Arkanoid" + "//xmlLevel" + lvl+".xml";
        FileStream file = File.Create(path);
        blockName += "|";
        blockName += blockPosition;
        writer.Serialize(file, blockName);
        file.Close();
    }
}

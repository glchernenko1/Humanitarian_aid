using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RedBlockXor0 : MonoBehaviour
{
    public GameObject textObject;
    Text textComponent;
    public bool flag =false;
    BattledoreMove playerScript;
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<BattledoreMove>();
        if (textObject != null){
            textComponent = textObject.GetComponent<Text>();
            textComponent.text = "O";
        }
    }
    
    
    
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        {
            flag = !flag;
            textComponent.text = flag? "X" : "O";
            var tmp = GameObject.FindGameObjectsWithTag("RedBlock");
            bool endflag = true;
            foreach (var i in tmp)
            {
                endflag =endflag && i.GetComponent<RedBlockXor0>().flag;
            }
            if (endflag)
            {
                foreach (var i in tmp)
                {
                    Destroy(i); 
                }
                playerScript.BlockDestroyed(150);
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

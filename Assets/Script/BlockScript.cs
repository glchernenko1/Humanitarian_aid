using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject textObject;
    Text textComponent;
    public int hitsToDestroy;
    public int points;
    BattledoreMove playerScript;

    public virtual void CreateBonus()
    {
        
    }
    
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<BattledoreMove>(); 
        if (textObject != null)
        {
            textComponent = textObject.GetComponent<Text>();
            textComponent.text = hitsToDestroy.ToString();
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        {
            
            try
            {
                Debug.Log(collision.gameObject.GetComponent<BallScript>().hit);
                hitsToDestroy -= collision.gameObject.GetComponent<BallScript>().hit;
            }
            catch 
            {
                Debug.Log(collision.gameObject.GetComponent<Bullet>().hit);
                hitsToDestroy -= collision.gameObject.GetComponent<Bullet>().hit;
            }

            if (hitsToDestroy <= 0)
            {
                playerScript.BlockDestroyed(points);
                CreateBonus();
                Destroy(gameObject);
                
            }
            else if (textComponent != null)
                textComponent.text = hitsToDestroy.ToString();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

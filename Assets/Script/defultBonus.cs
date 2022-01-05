using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defultBonus : MonoBehaviour
{

    public virtual void GetBounus()
    {
        Debug.Log("ok");
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        GetBounus();
        Destroy(gameObject);
        
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
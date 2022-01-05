using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int hit;
    public Vector2 ballInitialForce;
    Rigidbody2D rb;
    AudioSource audioSrc;
    public AudioClip hitSound;
    public GameDataScript gameData;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSrc = Camera.main.GetComponent<AudioSource>();
        rb.AddForce(ballInitialForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void tmp()
    {
        Destroy(this.gameObject);
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameData.sound)
            audioSrc.PlayOneShot(hitSound);
        transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Invoke("tmp", 0.1f);
        
    }
}

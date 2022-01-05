using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Vector2 ballInitialForce;
    Rigidbody2D rb;
    public GameObject playerObj;
    float deltaX;

    public int hit;
    
    
    AudioSource audioSrc;
    public AudioClip hitSound;
    public AudioClip loseSound;
    public GameDataScript gameData;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        deltaX = transform.position.x;
        audioSrc = Camera.main.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!rb.isKinematic && Input.GetKeyDown(KeyCode.J))
        {
            var v = rb.velocity;
            if (Random.Range(0,2) == 0)
                v.Set(v.x - 0.1f, v.y + 0.1f);
            else
                v.Set(v.x + 0.1f, v.y - 0.1f);
            rb.velocity = v;
        }

        
        
        if (rb.isKinematic)
            if (Input.GetButtonDown("Fire1"))
            {
                rb.isKinematic = false;
                rb.AddForce(ballInitialForce);
            }
            else
            {
                var pos = transform.position;
                pos.x = playerObj.transform.position.x + deltaX;
                transform.position = pos;
                
            }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameData.sound)
            audioSrc.PlayOneShot(hitSound);
        if (GameObject.Find("Battledore/LazerGun").GetComponent<Wepon>().isActivity || GameObject.Find("Battledore/Cannon").GetComponent<Wepon>().isActivity)
        transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
    }

    
    
    void OnTriggerEnter2D(Collider2D other)
    { 
        if (gameData.sound)
            audioSrc.PlayOneShot(loseSound);
        Destroy(gameObject);
        playerObj.GetComponent<BattledoreMove>().CheckBalls();
    }
}

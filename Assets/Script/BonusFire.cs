using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFire : defultBonus
{
    public override void GetBounus()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var i in balls)
        {
            i.GetComponent<BallScript>().hit = 4;
            i.GetComponent<SpriteRenderer>().color = new Color(1,0.5f,0.1f, 1);
        }
    }
}

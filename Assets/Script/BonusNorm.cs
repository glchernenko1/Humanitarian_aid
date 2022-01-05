using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusNorm : defultBonus
{
    public override void GetBounus()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var i in balls)
        {
            i.GetComponent<BallScript>().hit = 1;
            i.GetComponent<SpriteRenderer>().color = new Color(1,1,1, 1);
        }
    }
}

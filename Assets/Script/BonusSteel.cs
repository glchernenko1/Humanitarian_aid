using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSteel : defultBonus
{
    public override void GetBounus()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length!=0)
            foreach (var i in balls)
            {
                i.GetComponent<BallScript>().hit = 8;
                i.GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f, 1);
            }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCannon : defultBonus
{
    
    public override void GetBounus()
    {
        var gun = GameObject.Find("Battledore/Cannon");
        var gun1 = GameObject.Find("Battledore/LazerGun");
        gun1.GetComponent<SpriteRenderer>().enabled = false;
        gun1.GetComponent<Wepon>().isActivity = false;
        
        Debug.Log(gun);

        gun.GetComponent<SpriteRenderer>().enabled = true;
        gun.GetComponent<Wepon>().isActivity = true;
    }
}
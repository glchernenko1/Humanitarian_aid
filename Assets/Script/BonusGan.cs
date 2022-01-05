using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGan : defultBonus
{
    
    public override void GetBounus()
    {
        var gun = GameObject.Find("Battledore/LazerGun");
        
        var gun2 = GameObject.Find("Battledore/Cannon");
        gun2.GetComponent<SpriteRenderer>().enabled = false;
        gun2.GetComponent<Wepon>().isActivity = false;
        
        Debug.Log(gun);

        gun.GetComponent<SpriteRenderer>().enabled = true;
        gun.GetComponent<Wepon>().isActivity = true;
    }
}

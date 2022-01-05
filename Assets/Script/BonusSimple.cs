using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSimple : defultBonus
{
    public override void GetBounus()
    {
        var gun1 = GameObject.Find("Battledore/LazerGun");
        var gun2 = GameObject.Find("Battledore/Cannon");
        
        gun1.GetComponent<SpriteRenderer>().enabled = false;
        gun1.GetComponent<Wepon>().isActivity = false;
        gun2.GetComponent<SpriteRenderer>().enabled = false;
        gun2.GetComponent<Wepon>().isActivity = false;
        var tmp = GameObject.Find("Battledore");
        tmp.GetComponent<BattledoreMove>().CreateBalls();
    }
}

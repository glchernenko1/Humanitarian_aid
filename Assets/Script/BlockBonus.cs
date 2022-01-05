using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BlockBonus : BlockScript
{
    // Start is called before the first frame update

    [NotNull] public GameObject BonusPlu100;
    public GameObject BonusFire;
    public GameObject BonusSteel;
    public GameObject BonusNorm;
    public GameObject BonusGun;
    public GameObject BonusCannon;
    public GameObject BonusBomp;
    public GameObject BonusSimple;
    public int bonusNumber;

    public override void CreateBonus()
    {
        if (bonusNumber > -1)
        {
            Debug.Log(bonusNumber);
            Debug.Log("+=+++==+++=++");
            GameObject newBonus = new GameObject();
            switch (bonusNumber)
            {
                case (0):
                    newBonus = Instantiate(BonusPlu100);
                   
                    break;
                case (1):
                    newBonus = Instantiate(BonusFire);
                    
                    break;
                case (2):
                    newBonus = Instantiate(BonusSteel);
                   
                    break;
                case (3):
                    newBonus = Instantiate(BonusNorm);
                    // newBonus.AddComponent<BonusBallScript>();
                    break;
                case (4):
                    newBonus = Instantiate(BonusGun);
                    break;
                case (5):
                    newBonus = Instantiate(BonusCannon);
                    break;
                case (6):
                    newBonus = Instantiate(BonusBomp);
                    break;
                case (7):
                    newBonus = Instantiate(BonusSimple);
                    break;
                
            }
            newBonus.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
        } 
        
        
        
    }
   
}

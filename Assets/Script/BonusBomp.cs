using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BonusBomp : defultBonus
{
    public override void GetBounus()
    {
        SceneManager.LoadScene("MainScene");
    }
}

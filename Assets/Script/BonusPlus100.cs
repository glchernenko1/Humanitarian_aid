using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlus100 : defultBonus
{
    public GameDataScript gameData;
    public int points = 100;
    
    
    
    int requiredPointsToBall
    { get { return 400 + (gameData.level - 1) * 20; } }
    public override void GetBounus()
    {
        gameData.points += points;
        gameData.pointsToBall += points;
        if (gameData.pointsToBall >= requiredPointsToBall)
        {
            gameData.balls++;
            gameData.pointsToBall -= requiredPointsToBall;
        }
    }
}

using UnityEngine;

public class StealthEnemy : Enemy
{
    protected override void InitializeAttributes()
    {
        moveSpeed = 125f;
        deathCoinReward = 7;
        damage = 15;
        maxHealth = 30f;
        hide = true;
        heavy = false;
    }
}
using UnityEngine;

public class DefaultEnemy : Enemy
{
    protected override void InitializeAttributes()
    {
        moveSpeed = 100f;
        deathCoinReward = 5;
        damage = 10;
        maxHealth = 50f;
        heavy = false;
        hide = false;
    }
    
}
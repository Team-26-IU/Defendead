using UnityEngine;

    public class HeavyEnemy : Enemy
    {
        protected override void InitializeAttributes()
        {
            moveSpeed = 75f;
            deathCoinReward = 10;
            damage = 20;
            maxHealth = 100f;
            hide = false;
            heavy = true;
        }
    }

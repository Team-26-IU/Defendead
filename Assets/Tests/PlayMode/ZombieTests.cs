using NUnit.Framework;
using UnityEngine;

public class EnemyTests
{
    
    private GameObject enemyGameObject;
    private DefaultEnemy enemy; // Assuming TestEnemy is a concrete implementation of Enemy
    private GameObject mainBuildingObject;
    private MainBuilding mainBuilding;

    [SetUp]
    public void Setup()
    {
        // Initialize your GameObjects and Components here
        enemyGameObject = new GameObject();
        enemy = enemyGameObject.AddComponent<DefaultEnemy>();
        mainBuildingObject = new GameObject();
        mainBuilding = mainBuildingObject.AddComponent<MainBuilding>();
        enemy.mainBuilding = mainBuilding;
        enemy.EnemyHealth = enemyGameObject.AddComponent<EnemyHealth>();
    }
    

    [Test]
    public void Enemy_start_stop_moving()
    {
        enemy.ResumeMovement(); 

        Assert.AreEqual(100f, enemy.MoveSpeed);
      
        enemy.StopMovement();

        Assert.AreEqual(0, enemy.MoveSpeed);
    }

    [Test]
    public void Enemy_resume_moving()
    {
        float expectedMoveSpeed = 100.0f; //speed of default zombie moving
        enemy.StopMovement(); 

        enemy.ResumeMovement(); 

        Assert.AreEqual(expectedMoveSpeed, enemy.MoveSpeed, "MoveSpeed should be restored to its original value after calling ResumeMovement.");
    }
    [Test]
    public void Enemy_InitialMoveSpeed_IsSetCorrectly()
    {
        var defaultEnemy = new GameObject().AddComponent<DefaultEnemy>();
        var heavyEnemy = new GameObject().AddComponent<HeavyEnemy>();
        var stealthEnemy = new GameObject().AddComponent<StealthEnemy>();
        defaultEnemy.ResumeMovement(); 
        heavyEnemy.ResumeMovement(); 
        stealthEnemy.ResumeMovement(); 


        
        // Assert
        Assert.AreEqual(100, defaultEnemy.MoveSpeed,"Initial MoveSpeed of default zombie should be set correctly.");
        Assert.AreEqual(50, heavyEnemy.MoveSpeed,"Initial MoveSpeed of heavy zombie should be set correctly.");
        Assert.AreEqual(150, stealthEnemy.MoveSpeed,"Initial MoveSpeed of stealth zombie should be set correctly.");

    }

    [Test]
    public void Enemy_ChangeMoveSpeedWhileMoving_UpdatesMoveSpeedCorrectly()
    {
        // Arrange
        float newMoveSpeed = 120.0f;
        enemy.ResumeMovement();

        // Act
        enemy.MoveSpeed = newMoveSpeed;

        // Assert
        Assert.AreEqual(newMoveSpeed, enemy.MoveSpeed, 0.001f, "MoveSpeed should be updated to the new value while moving.");
    }

    
    [Test]
    public void Enemy_DealDamage()
    {
        enemy.EnemyHealth.DealDamage(50); 
        
        Assert.AreEqual(enemy.MaxHealth-50, enemy.EnemyHealth.CurrentHealth, "Enemy health was not equal to expected.");
    }
    }
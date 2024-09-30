using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthDecorator : HealthPoints
{
    private EnemyPoolingService enemyPoolingService;

    protected override void Start()
    {
        base.Start();   
        enemyPoolingService = ServiceLocator.Instance.AccessService<EnemyPoolingService>();
    }

    public override void Die()
    {
        base.Die();
        enemyPoolingService.StoreEnemy(gameObject);
    }
}

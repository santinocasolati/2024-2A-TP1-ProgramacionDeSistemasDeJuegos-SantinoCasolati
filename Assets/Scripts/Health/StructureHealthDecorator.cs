using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHealthDecorator : HealthPoints
{
    [SerializeField] private float respawnTime = 3f;

    private RespawnQueueService respawnQueueService;

    public override void Die()
    {
        base.Die();

        respawnQueueService = ServiceLocator.Instance.AccessService<RespawnQueueService>();

        gameObject.SetActive(false);
        respawnQueueService.RespawnCharacter(gameObject, respawnTime);
    }
}

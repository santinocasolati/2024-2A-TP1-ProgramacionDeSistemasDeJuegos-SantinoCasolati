using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHealthDecorator : HealthPoints
{
    [SerializeField] private float respawnTime = 3f;

    public override void Die()
    {
        base.Die();
        StartCoroutine(Respawn());
        gameObject.SetActive(false);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        ResetHealth();
        gameObject.SetActive(true);
    }
}

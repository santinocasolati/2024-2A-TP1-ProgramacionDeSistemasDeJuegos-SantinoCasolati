using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnQueueService : BaseService
{
    public void RespawnCharacter(GameObject character, float timeToRespawn)
    {
        StartCoroutine(Respawn(character, timeToRespawn));
    }

    private IEnumerator Respawn(GameObject character, float timeToRespawn)
    {
        yield return new WaitForSeconds(timeToRespawn);

        character.GetComponent<HealthPoints>().ResetHealth();
        character.SetActive(true);
    }
}

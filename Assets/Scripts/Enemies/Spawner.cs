using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int spawnsPerPeriod = 10;
    [SerializeField] private float frequency = 30;
    [SerializeField] private float period = 0;

    private EnemyPoolingService enemyPoolingService;

    private void OnEnable()
    {
        if (frequency > 0) period = 1 / frequency;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void FetchComponents()
    {
        enemyPoolingService ??= ServiceLocator.Instance.AccessService<EnemyPoolingService>();
    }

    private IEnumerator SpawnEnemies()
    {
        FetchComponents();

        while (!destroyCancellationToken.IsCancellationRequested)
        {
            for (int i = 0; i < spawnsPerPeriod; i++)
            {
                GameObject enemyInstance = enemyPoolingService.GetEnemy(transform.position, transform.rotation);
                enemyInstance.transform.SetPositionAndRotation(transform.position, transform.rotation);
                enemyInstance.SetActive(true);
            }

            yield return new WaitForSeconds(period);
        }
    }
}

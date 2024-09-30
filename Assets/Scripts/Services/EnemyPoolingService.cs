using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolingService : BaseService
{
    [SerializeField] private GameObject enemyPrefab;

    private GenericPool<GameObject> enemyPool;

    private void Awake()
    {
        enemyPool = new GenericPool<GameObject>((parameters) =>
        {
            Vector3 position = (Vector3)parameters[0];
            Quaternion rotation = (Quaternion)parameters[1];

            return Instantiate(enemyPrefab, position, rotation);
        });
    }

    public GameObject GetEnemy(Vector3 position, Quaternion rotation)
    {
        Debug.Log(enemyPool);
        return enemyPool.GetObject(position, rotation);
    }

    public void StoreEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.ReleaseObject(enemy);
    }
}

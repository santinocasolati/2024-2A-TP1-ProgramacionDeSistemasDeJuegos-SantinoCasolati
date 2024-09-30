using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        public event Action OnSpawn = delegate { };
        public event Action OnDeath = delegate { };
    
        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();
    
        private void FetchComponents()
        {
            agent ??= GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            //Is this necessary?? We're like, searching for it from every enemy D:
            var townCenter = GameObject.FindGameObjectWithTag("TownCenter");
            if (townCenter == null)
            {
                Debug.LogError($"{name}: Found no {nameof(townCenter)}!! :(");
                return;
            }

            var destination = townCenter.transform.position;
            destination.y = transform.position.y;
            agent.SetDestination(destination);
            StartCoroutine(AlertSpawn());
        }

        private IEnumerator AlertSpawn()
        {
            //Waiting one frame because event subscribers could run their onEnable after us.
            yield return null;
            OnSpawn();
        }

        private void Update()
        {
            if (agent.hasPath
                && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                Debug.Log($"{name}: I'll die for my people!");
                Die();
            }
        }

        private void Die()
        {
            OnDeath();
            ServiceLocator.Instance.AccessService<EnemyPoolingService>().StoreEnemy(gameObject);
        }
    }
}

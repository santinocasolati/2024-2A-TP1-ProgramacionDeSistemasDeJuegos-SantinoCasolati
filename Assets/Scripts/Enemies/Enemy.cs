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
        [SerializeField] private EnemyDataSO enemyData;

        private HealthPoints healthPoints;
        private StructuresLocationService structuresLocationService;
        private GameObject target;
        private bool targetLocked = false;
    
        private void FetchComponents()
        {
            agent ??= GetComponent<NavMeshAgent>();
            healthPoints ??= GetComponent<HealthPoints>();
            structuresLocationService ??= ServiceLocator.Instance.AccessService<StructuresLocationService>();
        }

        private void OnEnable()
        {
            SetTarget();
        }

        private void OnDisable()
        {
            targetLocked = false;
        }

        private void Update()
        {
            if (target != null && target.activeSelf)
            {
                if (agent.hasPath
                && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
                {
                    if (target.TryGetComponent(out HealthPoints targetHealth))
                    {
                        Debug.Log($"{name}: I'll die for my people!");
                        healthPoints.Damage(enemyData.DamageRecievedOnHit);
                        targetHealth.Damage(enemyData.DamageToObjectives);
                    }
                }
            } else
            {
                healthPoints.Die();
            }
        }

        private void SetTarget()
        {
            FetchComponents();

            if (!structuresLocationService) return;

            target = structuresLocationService.GetClosestStructureLocation(transform.position);

            if (target == null) return;

            Vector3 closestStructure = target.transform.position;
            closestStructure.y = transform.position.y;
            agent.SetDestination(closestStructure);
            targetLocked = true;
        }
    }
}

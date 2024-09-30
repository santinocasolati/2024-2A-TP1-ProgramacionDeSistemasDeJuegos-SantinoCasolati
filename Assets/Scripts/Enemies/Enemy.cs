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
        [SerializeField] private float damageReceivedOnHit = 1;
        [SerializeField] private float damageToObjectives = 2;

        private HealthPoints healthPoints;
        private StructuresLocationService structuresLocationService;
        private GameObject target;
    
        private void FetchComponents()
        {
            agent ??= GetComponent<NavMeshAgent>();
            healthPoints ??= GetComponent<HealthPoints>();
            structuresLocationService ??= ServiceLocator.Instance.AccessService<StructuresLocationService>();
        }

        private void OnEnable()
        {
            SetTarget();

            if (structuresLocationService)
                structuresLocationService.OnStoreModified += SetTarget;
        }

        private void OnDisable()
        {
            if (structuresLocationService)
                structuresLocationService.OnStoreModified -= SetTarget;
        }

        private void Update()
        {
            if (agent.hasPath
                && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                Debug.Log($"{name}: I'll die for my people!");
                healthPoints.Damage(damageReceivedOnHit);

                if (target != null)
                    target.GetComponent<HealthPoints>().Damage(damageToObjectives);
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
        }
    }
}

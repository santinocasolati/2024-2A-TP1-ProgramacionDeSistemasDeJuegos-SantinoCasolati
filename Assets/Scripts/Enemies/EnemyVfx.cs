using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyVfx : MonoBehaviour
    {
        private HealthPoints _enemyHealth;
        [SerializeField] private RandomContainer<ParticleSystem> deathPrefabs;

        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();

        private void FetchComponents()
        {
            _enemyHealth = GetComponent<HealthPoints>();
        }

        private void OnEnable()
        {
            _enemyHealth.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            _enemyHealth.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            if(!deathPrefabs.TryGetRandom(out var prefab))
                return;
            var vfx = Instantiate(prefab, transform.position, transform.rotation);
            var mainModule = vfx.main;
            mainModule.stopAction = ParticleSystemStopAction.Destroy;
        }
    }
}

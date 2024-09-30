using Audio;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemySfx : MonoBehaviour
    {
        [SerializeField] private AudioPlayer audioSourcePrefab;
        [SerializeField] private RandomContainer<AudioClipData> spawnClips;
        [SerializeField] private RandomContainer<AudioClipData> explosionClips;
        private HealthPoints _enemyHealth;

        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();
    
        private void FetchComponents()
        {
            // "a ??= b" is equivalent to "if(a == null) a = b" 
            _enemyHealth ??= GetComponent<HealthPoints>();
        }
        
        private void OnEnable()
        {
            if (!audioSourcePrefab)
            {
                Debug.LogError($"{nameof(audioSourcePrefab)} is null!");
                return;
            }
            _enemyHealth.OnSpawn += HandleSpawn;
            _enemyHealth.OnDeath += HandleDeath;
        }
        
        private void OnDisable()
        {
            _enemyHealth.OnSpawn -= HandleSpawn;
            _enemyHealth.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            PlayRandomClip(explosionClips, audioSourcePrefab);
        }

        private void HandleSpawn()
        {
            PlayRandomClip(spawnClips, audioSourcePrefab);
        }

        private void PlayRandomClip(RandomContainer<AudioClipData> container, AudioPlayer sourcePrefab)
        {
            if (!container.TryGetRandom(out var clipData))
                return;
            
            SpawnSource(sourcePrefab).Play(clipData);
        }

        private AudioPlayer SpawnSource(AudioPlayer prefab)
        {
            return Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}

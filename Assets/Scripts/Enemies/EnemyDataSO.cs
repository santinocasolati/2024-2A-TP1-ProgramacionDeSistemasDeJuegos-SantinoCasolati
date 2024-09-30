using Audio;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] private float _damageRecievedOnHit = 1;
    [SerializeField] private float _damageToObjectives = 2;
    [SerializeField] private RandomContainer<AudioClipData> _spawnClips;
    [SerializeField] private RandomContainer<AudioClipData> _explosionClips;
    [SerializeField] private RandomContainer<ParticleSystem> _deathPrefabs;

    public float DamageRecievedOnHit { get => _damageRecievedOnHit; }
    public float DamageToObjectives { get => _damageToObjectives; }
    public RandomContainer<AudioClipData> SpawnClips { get => _spawnClips; }
    public RandomContainer<AudioClipData> ExplosionClips { get => _explosionClips; }
    public RandomContainer<ParticleSystem> DeathPrefabs { get => _deathPrefabs; }
}

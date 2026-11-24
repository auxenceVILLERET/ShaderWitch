using UnityEngine;
using NaughtyAttributes;

public class S_SpawnAbilities : MonoBehaviour
{
    [BoxGroup("Light Projectile Settings")]
    [SerializeField] private GameObject _lightProjectilePrefab;
    [BoxGroup("Light Projectile Settings")]
    [SerializeField] private Transform _lightSpawnPoint;

    [BoxGroup("Heavy Projectile Settings")]
    [SerializeField] private GameObject _heavyProjectilePrefab;
    [BoxGroup("Heavy Projectile Settings")]
    [SerializeField] private Transform _heavySpawnPoint;

    [BoxGroup("Skill Projectile Settings")]
    [SerializeField] private GameObject _skillProjectilePrefab;
    [BoxGroup("Skill Projectile Settings")]
    [SerializeField] private Transform _skillSpawnPoint;

    public void SpawnLightProjectile()
    {
        if (_lightProjectilePrefab != null && _lightSpawnPoint != null)
        {
            Instantiate(_lightProjectilePrefab, _lightSpawnPoint.position, _lightSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Light projectile prefab or spawn point is not assigned.");
        }
    }

    public void SpawnHeavyProjectile()
    {
        if (_heavyProjectilePrefab != null && _heavySpawnPoint != null)
        {
            Instantiate(_heavyProjectilePrefab, _heavySpawnPoint.position, _heavySpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Heavy projectile prefab or spawn point is not assigned.");
        }
    }

    public void SpawnSkillProjectile()
    {
        if (_skillProjectilePrefab != null && _skillSpawnPoint != null)
        {
            Instantiate(_skillProjectilePrefab, _skillSpawnPoint.position, _skillSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Skill projectile prefab or spawn point is not assigned.");
        }
    }

}

using UnityEngine;
using NaughtyAttributes;

public class S_Health : MonoBehaviour
{
    [BoxGroup("Health")]
    [SerializeField] private float _maxHealth = 100f;
    [BoxGroup("Health")]
    [SerializeField] private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Debug.Log("Entity is dead.");
        }
    }

    public void Heal(float amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}

using UnityEngine;
using NaughtyAttributes;
using System;

public class S_Mana : MonoBehaviour
{
    [BoxGroup("Mana")]
    [SerializeField] private float _maxMana = 100f;
    [BoxGroup("Mana")]
    [SerializeField] private float _currentMana;

    public float MaxMana => _maxMana;
    public float CurrentMana => _currentMana;

    public event Action<float, float> OnManaChanged;

    void Awake()
    {
        _currentMana = _maxMana;
    }

    public void UseMana(float amount)
    {
        _currentMana -= amount;
        if (_currentMana < 0)
        {
            _currentMana = 0;
        }
        OnManaChanged?.Invoke(_currentMana, _maxMana);
    }

    public void RegenerateMana(float amount)
    {
        _currentMana += amount;
        if (_currentMana > _maxMana)
        {
            _currentMana = _maxMana;
        }
        OnManaChanged?.Invoke(_currentMana, _maxMana);
    }
}

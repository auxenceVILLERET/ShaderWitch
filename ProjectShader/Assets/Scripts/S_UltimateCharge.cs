using UnityEngine;
using NaughtyAttributes;
using System;

public class S_UltimateCharge : MonoBehaviour
{
    [BoxGroup("Ultimate Charge")]
    [SerializeField] private float _ultimateCharge = 0f;
    [BoxGroup("Ultimate Charge")]
    [SerializeField] private SO_Abilities _abilities;

    private float _maxUltimateCharge;

    public event Action<float, float> OnUltimateChargeChanged;

    void Awake()
    {
        _maxUltimateCharge = _abilities.UltimateChargeMax;
        OnUltimateChargeChanged?.Invoke(_ultimateCharge, _maxUltimateCharge);
    }

    public float UltimateCharge => _ultimateCharge;
    public float MaxUltimateCharge => _maxUltimateCharge;

    public void IncreaseUltimateCharge(float amount)
    {
        _ultimateCharge += amount;
        if (_ultimateCharge > _maxUltimateCharge)
        {
            _ultimateCharge = _maxUltimateCharge;
        }
        OnUltimateChargeChanged?.Invoke(_ultimateCharge, _maxUltimateCharge);
    }
    public void ResetUltimateCharge()
    {
        _ultimateCharge = 0f;
        OnUltimateChargeChanged?.Invoke(_ultimateCharge, _maxUltimateCharge);
    }
}

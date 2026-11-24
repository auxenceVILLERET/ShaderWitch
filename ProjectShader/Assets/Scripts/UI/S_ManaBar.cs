using UnityEngine;
using NaughtyAttributes;
using System;
using UnityEngine.UI;

public class S_ManaBar : MonoBehaviour
{
    [BoxGroup("Dependencies")]
    [SerializeField] private S_Mana _mana;
    [BoxGroup("Dependencies")]
    [SerializeField] private Slider _manaSlider;

    void OnEnable()
    {
        _mana.OnManaChanged += UpdateManaBar;
    }

    void OnDisable()
    {
        _mana.OnManaChanged -= UpdateManaBar;
    }

    void UpdateManaBar(float currentMana, float maxMana)
    {
        float manaPercentage = currentMana / maxMana;
        _manaSlider.value = manaPercentage;
    }
}

using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class S_UltimateBar : MonoBehaviour
{
    [BoxGroup("Dependencies")]
    [SerializeField] private S_UltimateCharge _ultimateCharge;
    [BoxGroup("Dependencies")]
    [SerializeField] private Slider _ultimateSlider;

    void OnEnable()
    {
        _ultimateCharge.OnUltimateChargeChanged += UpdateUltimateBar;
    }

    void OnDisable()
    {
        _ultimateCharge.OnUltimateChargeChanged -= UpdateUltimateBar;
    }

    void UpdateUltimateBar(float currentUltimate, float maxUltimate)
    {
        float ultimatePercentage = currentUltimate / maxUltimate;
        _ultimateSlider.value = ultimatePercentage;
    }
}

using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class S_SkillCooldown : MonoBehaviour
{
    [BoxGroup("Dependencies")]
    [SerializeField] private S_Abilities _abilities;
    [BoxGroup("Dependencies")]
    [SerializeField] private Image _cooldownImage;

    private void Update()
    {
        if (_abilities == null || _cooldownImage == null)
        {
            return;
        }

        float cooldownRemaining = _abilities.SkillCooldownRemaining;
        float cooldownDuration = _abilities.SkillCooldownDuration;

        if (cooldownDuration > 0f)
        {
            float fillAmount = Mathf.Clamp01(cooldownRemaining / cooldownDuration);
            _cooldownImage.fillAmount = fillAmount;
        }
        else
        {
            _cooldownImage.fillAmount = 0f;
        }
    }
}

using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Playables;

public class S_Abilities : MonoBehaviour
{
    [BoxGroup("Dependencies")]
    [SerializeField] private GameObject _entity;
    [BoxGroup("Dependencies")]
    [SerializeField] private PlayableDirector _playableDirector;
    [BoxGroup("Dependencies")]
    [SerializeField] private S_Mana _mana;
    [BoxGroup("Dependencies")]
    [SerializeField] private S_UltimateCharge _ultimateCharge;
    [BoxGroup("Dependencies")]
    [SerializeField] private S_UltimateFog _ultimateFog;
    [BoxGroup("Abilities")]
    [SerializeField] private SO_Abilities _currentAbility;
    
    private bool _isUsingAbility = false;
    public bool IsUsingAbility => _isUsingAbility;
    private float _skillCooldownTimer = 0f;
    public float SkillCooldownRemaining => Mathf.Max(0f, _skillCooldownTimer);
    public float SkillCooldownDuration => _currentAbility != null ? _currentAbility.SkillCooldown : 0f;

    public void UseLightAttack()
    {
        UseAbility(_currentAbility.LightAttack);
        _isUsingAbility = true;
    }

    public void UseHeavyAttack()
    {
        UseAbility(_currentAbility.HeavyAttack);
        _isUsingAbility = true;
    }

    public void UseSkillAttack()
    {
        if (_skillCooldownTimer > 0f)
        {
            return;
        }

        if (UseAbility(_currentAbility.Skill))
        {
            _skillCooldownTimer = _currentAbility.SkillCooldown;
        }
        _isUsingAbility = true;
    }

    public void UseUltimateAttack()
    {
        if (_ultimateCharge.UltimateCharge < _ultimateCharge.MaxUltimateCharge)
        {
            return;
        }

        if (UseAbility(_currentAbility.Ultimate))
        {
            _isUsingAbility = true;
            _ultimateCharge.ResetUltimateCharge();

            _ultimateFog.ActiverUlt();
        }
    }

    private void RestoreFog()
    {
        RenderSettings.fog = true;
    }

    private bool UseAbility(S_AttackData ability)
    {
        if (ability == null) return false;
        if (_playableDirector == null) return false;
        if(_isUsingAbility) return false;
        if (_mana.CurrentMana < ability.ManaCost) return false;

        if(ability.Timeline != null)
        {
            _playableDirector.Stop();
            _playableDirector.playableAsset = ability.Timeline;
            _playableDirector.time = 0;
            _playableDirector.Play();
            _mana.UseMana(ability.ManaCost);
            _ultimateCharge.IncreaseUltimateCharge(ability.UltimateChargeAmount);
        }

        _playableDirector.Play();
        return true;
    }

    void Update()
    {
        _mana.RegenerateMana(Time.deltaTime * 5f);

        if(_skillCooldownTimer > 0f)
        {
            _skillCooldownTimer -= Time.deltaTime;
        }

        if(_playableDirector.state == PlayState.Playing)
        {
            _isUsingAbility = true;
        }
        else
        {
            _isUsingAbility = false;
        }
    }
}

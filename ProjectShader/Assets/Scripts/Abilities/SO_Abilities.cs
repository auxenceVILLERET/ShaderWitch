using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Playables;

public enum E_AttackType { Light, Heavy, Skill, Ultimate }

[System.Serializable]
public class S_AttackData
{
    public E_AttackType Type;
    public PlayableAsset Timeline;
    public float Damage = 10f;
    public float ManaCost = 10f;
    public float UltimateChargeAmount = 10f;
}


[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities/Ability Data")]
public class SO_Abilities : ScriptableObject
{

    [BoxGroup("Attacks")] public S_AttackData LightAttack;
    [BoxGroup("Attacks")] public S_AttackData HeavyAttack;

    [BoxGroup("Skills")] public S_AttackData Skill;
    [BoxGroup("Skills")] public float SkillCooldown = 15f;
    [BoxGroup("Skills")] public S_AttackData Ultimate;
    [BoxGroup("Skills")] public float UltimateChargeMax = 100f;
}

using UnityEngine;
using NaughtyAttributes;


public class S_Projectile : MonoBehaviour
{
    [BoxGroup("Settings")]
    [SerializeField] private float _speed = 10f;
    [BoxGroup("Settings")]
    [SerializeField] private float _lifetime = 5f;
    [BoxGroup("Settings")]
    [SerializeField] private SO_Abilities _abilities;
    [BoxGroup("Settings")]
    [SerializeField] private E_AttackType _attackType = E_AttackType.Light;
    [BoxGroup("Settings")]
    [SerializeField] private bool _destroyOnHit = true;
    [BoxGroup("Settings")]
    [SerializeField] private GameObject _hitEffectPrefab;
    [BoxGroup("Skill Settings")]
    [ShowIf("IsScaleAttack")]
    [SerializeField] private float _scaleSpeed = 1f;
    [SerializeField] private float _ultimateScaleSpeed = 0.25f;
    [BoxGroup("Skill Settings")]
    [ShowIf("IsScaleAttack")]
    [SerializeField] private float _fadeSpeed = 1f;


    private bool IsScaleAttack => _attackType == E_AttackType.Skill || _attackType == E_AttackType.Ultimate;

    private float _damage;
    public float Damage => _damage;
    public bool DestroyOnHit => _destroyOnHit;
    private Vector3 _targetScale;
    private bool _isScaleProjectile;
    private Vector3 _startPosition;
    private float _alpha = 1f;
    private Renderer _renderer;
    private MaterialPropertyBlock _propertyBlock;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        Destroy(gameObject, _lifetime);
        switch (_attackType)
        {
            case E_AttackType.Light:
                _damage = _abilities.LightAttack.Damage;
                break;
            case E_AttackType.Heavy:
                _damage = _abilities.HeavyAttack.Damage;
                break;
            case E_AttackType.Skill:
                _damage = _abilities.Skill.Damage;
                _isScaleProjectile = true;
                break;
            case E_AttackType.Ultimate:
                _damage = _abilities.Ultimate.Damage;
                _isScaleProjectile = true;
                break;
        }
        _startPosition = transform.position;
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        if (!_isScaleProjectile)
            return;

        if (_attackType == E_AttackType.Skill)
        {
            transform.localScale += Vector3.one * (_scaleSpeed * Time.deltaTime);
        }
        else if (_attackType == E_AttackType.Ultimate)
        {
            float distance = Vector3.Distance(_startPosition, transform.position);

            float scale = Mathf.Clamp(
                1f + distance * _ultimateScaleSpeed,
                1f,
                5f
            );

            transform.localScale = Vector3.one * scale;
        }

        _alpha = Mathf.Max(0f, _alpha - _fadeSpeed * Time.deltaTime);

        if (_renderer != null)
        {
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetFloat("_Alpha", _alpha);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) return;

        Vector3 impactPosition = transform.position;
        Vector3 impactNormal = -transform.forward;

        if (other.ClosestPoint(transform.position) != transform.position)
        {
            impactPosition = other.ClosestPoint(transform.position);
        }

        if (_hitEffectPrefab != null)
        {
            GameObject impact = Instantiate(
                _hitEffectPrefab,
                impactPosition,
                Quaternion.LookRotation(impactNormal)
            );

            Destroy(impact, 2f);
        }

        S_MushroomGlow mushroom = other.GetComponentInParent<S_MushroomGlow>();

        if (mushroom != null)
        {
            if (_attackType == E_AttackType.Ultimate)
            {
                mushroom.EnablePermanentGlow();
            }
            else
            {
                mushroom.LightUp();
            }
        }

        if (_destroyOnHit)
            Destroy(gameObject);
    }
}

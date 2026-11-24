using UnityEngine;
using NaughtyAttributes;

public class S_Move : MonoBehaviour
{
    [BoxGroup("Dependencies")]
    [SerializeField] private GameObject _entity;
    [BoxGroup("Dependencies")]
    [SerializeField] private Rigidbody _rigidbody;

    [BoxGroup("Footprints")]
    [SerializeField] private GameObject _footprintPrefab;
    [BoxGroup("Footprints")]
    [SerializeField] private Transform _footprintSpawnPoint;
    [BoxGroup("Footprints")]
    [SerializeField] private float _footprintLifetime = 8f;
    [BoxGroup("Footprints")]
    [SerializeField] private float _minimumDistanceBetweenFootprints = 0.5f;
    [BoxGroup("Footprints")]
    [SerializeField] private float _surfaceOffset = 0.02f;
    [BoxGroup("Footprints")]
    [SerializeField] private LayerMask _groundLayer = ~0;

    private Vector3 _currentDirection;
    private Vector3 _lastFootprintPosition;
    private bool _hasPlacedFootprint;

    private S_Stats _stats;
    private bool _isRunning;
    private Animator _animator;
    private S_Abilities _abilities;

    void Awake()
    {
        _stats = _entity.GetComponentInChildren<S_Stats>();
        _animator = _stats.Animator;
        _abilities = _entity.GetComponentInChildren<S_Abilities>();
    }

    public void SetDirection(Vector3 direction)
    {
        _currentDirection = direction;
        _isRunning = false;
    }

    public void SetRunDirection(Vector3 direction)
    {
        _currentDirection = direction;
        _isRunning = true;
    }

    void FixedUpdate()
    {
        if(_abilities.IsUsingAbility)
        {
            _animator.SetFloat("Speed", 0f);
            return;
        }
        
        Vector3 velocity = _rigidbody.linearVelocity;

        if (_currentDirection != Vector3.zero)
        {
            float speed = _isRunning ? _stats.RunSpeed : _stats.WalkSpeed;
            _animator.SetFloat("Speed", speed / _stats.RunSpeed);

            Vector3 dir = _currentDirection.normalized;
            velocity.x = dir.x * speed;
            velocity.z = dir.z * speed;

            Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
            _rigidbody.MoveRotation(Quaternion.RotateTowards(
                _rigidbody.rotation,
                targetRotation,
                _stats.RotationSpeed * Time.fixedDeltaTime
            ));
        }
        else
        {
            _animator.SetFloat("Speed", 0f);
            velocity.x = 0f;
            velocity.z = 0f;
        }

        _rigidbody.linearVelocity = velocity;
    }

    public void SpawnFootprint()
    {
        if (_footprintPrefab == null || _footprintSpawnPoint == null)
            return;

        if (_currentDirection.sqrMagnitude < 0.01f || _abilities.IsUsingAbility)
            return;

        Vector3 origin = _footprintSpawnPoint.position + Vector3.up * 0.2f;

        if (!Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 1f, _groundLayer))
            return;

        if (_hasPlacedFootprint &&
            Vector3.Distance(hit.point, _lastFootprintPosition) < _minimumDistanceBetweenFootprints)
            return;

        Quaternion rotation = Quaternion.LookRotation(-hit.normal, transform.forward);

        GameObject footprint = Instantiate(
            _footprintPrefab,
            hit.point + hit.normal * _surfaceOffset,
            rotation
        );

        if (_footprintLifetime > 0f)
            Destroy(footprint, _footprintLifetime);

        _lastFootprintPosition = hit.point;
        _hasPlacedFootprint = true;
    }
}

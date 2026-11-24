using UnityEngine;
using NaughtyAttributes;

public class S_Stats : MonoBehaviour
{
    [BoxGroup("Dependencies")]
    [SerializeField] private Animator _animator;

    [BoxGroup("Movement")]
    [SerializeField] private float _walkSpeed;
    [BoxGroup("Movement")]
    [SerializeField] private float _runSpeed;
    [BoxGroup("Movement")]
    [SerializeField] private float _rotationSpeed;

    public Animator Animator => _animator;
    
    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float RotationSpeed => _rotationSpeed;

    void Reset()
    {
        _walkSpeed = 5f;
        _runSpeed = 10f;
        _rotationSpeed = 720f;
    }

}

using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class S_BrainPlayer : MonoBehaviour
{
    [BoxGroup("Dependencies")]
    [SerializeField] private S_Move _move;
    [BoxGroup("Dependencies")]
    [SerializeField] private S_Abilities _abilities;

    [BoxGroup("Inputs")]
    [SerializeField] private InputActionReference _moveInput;
    [BoxGroup("Inputs")]
    [SerializeField] private InputActionReference _runInput;
    [BoxGroup("Inputs")]
    [SerializeField] private InputActionReference _lightHeavyInput;
    [BoxGroup("Inputs")]
    [SerializeField] private InputActionReference _skillInput;
    [BoxGroup("Inputs")]
    [SerializeField] private InputActionReference _ultimateInput;

    private Vector3 _currentDirection;
    private Transform _cameraTransform;

    void Awake()
    {
        if(_cameraTransform == null && Camera.main != null)
        {
            _cameraTransform = Camera.main.transform;
        }
    }

    void OnEnable()
    {
        _lightHeavyInput.action.performed += OnLightHeavyPerformed;
        _skillInput.action.performed += OnSkillPerformed;
        _ultimateInput.action.performed += OnUltimatePerformed;
    }

    void OnDisable()
    {
        _lightHeavyInput.action.performed -= OnLightHeavyPerformed;
        _skillInput.action.performed -= OnSkillPerformed;
        _ultimateInput.action.performed -= OnUltimatePerformed;
    }

    void Update()
    {
        Vector2 input = _moveInput.action.ReadValue<Vector2>();

        if(input.sqrMagnitude < 0.0001f)
        {
            _move.SetDirection(Vector3.zero);
            return;
        }

        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = Vector3.ClampMagnitude(forward * input.y + right * input.x, 1f);
        _currentDirection = direction;

        if (_currentDirection != Vector3.zero)
        {
            if (_runInput.action.IsPressed())
            {
                _move.SetRunDirection(_currentDirection);
            }
            else
            {
                _move.SetDirection(_currentDirection);
            }
        }
    }

    void OnLightHeavyPerformed(InputAction.CallbackContext context)
    {
        if(context.interaction is TapInteraction)
        {
            _abilities.UseLightAttack();
        }
        else if(context.interaction is HoldInteraction)
        {
            _abilities.UseHeavyAttack();
        }
    }
    
    void OnSkillPerformed(InputAction.CallbackContext context)
    {
        _abilities.UseSkillAttack();
    }

    void OnUltimatePerformed(InputAction.CallbackContext context)
    {
        _abilities.UseUltimateAttack();
    }

}

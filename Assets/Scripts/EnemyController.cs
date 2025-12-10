using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stopDistance = 1.2f;
    [SerializeField] private GameObject _caughtUI;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CameraController _cameraController;

    public Transform Target
    {
        get => _target;
        set => _target = value;
    }

    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public float StopDistance
    {
        get => _stopDistance;
        set => _stopDistance = value;
    }

    public GameObject CaughtUI
    {
        get => _caughtUI;
        set => _caughtUI = value;
    }

    public PlayerInput PlayerInput
    {
        get => _playerInput;
        set => _playerInput = value;
    }

    public CameraController CameraController
    {
        get => _cameraController;
        set => _cameraController = value;
    }

    private bool _isActive = true;

    private void Awake()
    {
        if (CaughtUI != null)
        {
            CaughtUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (!_isActive || Target == null)
        {
            return;
        }

        Vector3 targetPosition = Target.position;
        targetPosition.y = transform.position.y;

        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > StopDistance)
        {
            transform.position += direction * MoveSpeed * Time.deltaTime;
            transform.forward = direction;
        }
        else
        {
            OnCaught();
        }
    }

    private void OnCaught()
    {
        _isActive = false;

        if (CaughtUI != null)
        {
            CaughtUI.SetActive(true);
        }

        if (PlayerInput != null)
        {
            PlayerInput.enabled = false;
        }

        if (CameraController != null)
        {
            CameraController.enabled = false;
        }

        Debug.Log("Player Caught");
    }
}

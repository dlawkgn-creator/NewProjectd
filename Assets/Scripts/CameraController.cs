using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _mouseSensitivity = 150f;
    [SerializeField] private float _minPitch = -30f;
    [SerializeField] private float _maxPitch = 60f;

    public Transform CameraTransform
    {
        get => _cameraTransform;
        set => _cameraTransform = value;
    }

    public float MouseSensitivity
    {
        get => _mouseSensitivity;
        set => _mouseSensitivity = value;
    }

    public float MinPitch
    {
        get => _minPitch;
        set => _minPitch = value;
    }

    public float MaxPitch
    {
        get => _maxPitch;
        set => _maxPitch = value;
    }

    private Vector2 _lookInput;
    private float _yaw;
    private float _pitch;

    private void Awake()
    {
        if (_cameraTransform == null)
        {
            _cameraTransform = Camera.main.transform;
        }

        Vector3 cameraAngles = _cameraTransform.localEulerAngles;
        _pitch = cameraAngles.x;

        Vector3 bodyAngles = transform.eulerAngles;
        _yaw = bodyAngles.y;
    }

    public void OnLook(InputValue value)
    {
        _lookInput = value.Get<Vector2>();
    }

    private void Update()
    {
        float deltaX = _lookInput.x * _mouseSensitivity * Time.deltaTime;
        float deltaY = _lookInput.y * _mouseSensitivity * Time.deltaTime;

        _yaw += deltaX;
        _pitch -= deltaY;

        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);

        transform.rotation = Quaternion.Euler(0f, _yaw, 0f);
        _cameraTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}

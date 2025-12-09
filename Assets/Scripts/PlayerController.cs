using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _airMoveMultiplier = 1.1f; //◀검색 결과

    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public float JumpHeight
    {
        get => _jumpHeight;
        set => _jumpHeight = value;
    }

    public float Gravity
    {
        get => _gravity;
        set => _gravity = value;
    }

    public float AirMoveMultiplier
    {
        get => _airMoveMultiplier;
        set => _airMoveMultiplier = value;
    }

    private CharacterController _cc;
    private Vector2 _moveInput;
    private float _verticalVelocity;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }

        if (_cc.isGrounded && _verticalVelocity <= 0f)
        {
            _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }
    }

    private void Update()
    {
        if (_cc.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -1f;
        }

        Vector3 move = new Vector3(_moveInput.x, 0f, _moveInput.y);

        if (move.sqrMagnitude > 0.001f)
        {
            move = transform.TransformDirection(move);
        }

        _verticalVelocity += Gravity * Time.deltaTime;
        move.y = _verticalVelocity;

        float currentSpeed = MoveSpeed;
        if (!_cc.isGrounded)
        {
            currentSpeed *= AirMoveMultiplier;
        }

        _cc.Move(move * currentSpeed * Time.deltaTime);
    }
}

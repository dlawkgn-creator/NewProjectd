using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed { get; set; } = 5f;

    private CharacterController _cc;
    private Vector2 _moveInput;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(_moveInput.x, 0f, _moveInput.y);

        if (move.sqrMagnitude > 0.001f)
        {
            move = transform.TransformDirection(move);
        }

        _cc.Move(move * MoveSpeed * Time.deltaTime);
    }
}

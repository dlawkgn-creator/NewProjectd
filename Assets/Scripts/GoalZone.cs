using UnityEngine;
using UnityEngine.InputSystem;

public class GoalZone : MonoBehaviour
{
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private GameObject _clearUI;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CameraController _cameraController;

    public string PlayerTag
    {
        get => _playerTag;
        set => _playerTag = value;
    }

    public GameObject ClearUI
    {
        get => _clearUI;
        set => _clearUI = value;
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

    private void Awake()
    {
        if (ClearUI != null)
        {
            ClearUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag))
        {
            return;
        }

        if (ClearUI != null)
        {
            ClearUI.SetActive(true);
        }

        if (PlayerInput != null)
        {
            PlayerInput.enabled = false;
        }

        if (CameraController != null)
        {
            CameraController.enabled = false;
        }

        Debug.Log("Goal Reached");
    }
}

using UnityEngine;

public class MovementController : MonoBehaviour
{
    private PlayerStats _playerStats;
    private Vector3 _movementDirection;
    private Rigidbody _playerRigidbody;

    private Vector3 _calculatedVelocity;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerRigidbody = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        GetMovementAxis();
    }

    private void FixedUpdate()
    {
        CalculateCurrentVelocity();
        _playerRigidbody.velocity = _calculatedVelocity;
    }

    private void GetMovementAxis()
    {
        var horizontalMovement = InputController.Instance.HorizontalInput;
        var verticalMovement = InputController.Instance.VerticalInput;
        _movementDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        _movementDirection.Normalize();
    }
    private void CalculateCurrentVelocity()
    {
        _calculatedVelocity = new Vector3
        (
            _movementDirection.x * _playerStats.MovementSpeed,
            _playerRigidbody.velocity.y,
            _movementDirection.z * _playerStats.MovementSpeed
        );
    }
}

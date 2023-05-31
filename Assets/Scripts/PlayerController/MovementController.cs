using UnityEngine;

public class MovementController : MonoBehaviour
{
    private PlayerStats _playerStats;
    private Vector3 _movementDirection;
    private Rigidbody _playerRigidbody;

    private Vector3 _calculatedVelocity;

    private GroundChecker _groundChecker;
    [SerializeField] float _wallCheckDistance;
    [SerializeField] private LayerMask _wallLayerMask;

    public float _coyoteJumpTime = 0.1f; // Time in seconds to allow coyote jump

    private bool _isJumping = false;
    private float _coyoteJumpTimer = 0f;

    float _lastTimeJump;

    private void Start()
    {
        _groundChecker = GetComponentInChildren<GroundChecker>();
        InputController.JumpButton += PreformJump;
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

        if (!_groundChecker.IsGrounded())
        {
            _coyoteJumpTimer += Time.deltaTime;
        }
        else
        {
            _coyoteJumpTimer = 0f;
        }
    }

    private void GetMovementAxis()
    {
        var horizontalMovement = InputController.MovementAxis.x;
        var verticalMovement = InputController.MovementAxis.y;
        _movementDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        _movementDirection.Normalize();
    }
    private void CalculateCurrentVelocity()
    {
        Vector3 targetVelocity = new Vector3
        (
            _movementDirection.x * _playerStats.MovementSpeed,
            0,
            _movementDirection.z * _playerStats.MovementSpeed
        );

        if (_movementDirection.magnitude < 0.1f)
        {
            _calculatedVelocity = Vector3.Lerp(_calculatedVelocity, targetVelocity, _playerStats.SmoothFactor);
            _calculatedVelocity.y = _playerRigidbody.velocity.y;
        }
        else
        {
            _calculatedVelocity = targetVelocity;
            _calculatedVelocity.y = _playerRigidbody.velocity.y;
        }
    }
    private void PreformJump()
    {
        if (Time.time - _lastTimeJump < _playerStats.JumpCooldown) return;

        if (!_groundChecker.IsGrounded() && _coyoteJumpTimer > _coyoteJumpTime)
        {
            return;
        }

        _lastTimeJump = Time.time;
        
        _isJumping = true;
        _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, 0,_playerRigidbody.velocity.z);
        _playerRigidbody.AddForce(new Vector3(0, 0, 0) + (Vector3.up  * _playerStats.JumpForce), ForceMode.Impulse);
    }
    void OnDestroy()
    {
        InputController.JumpButton -= PreformJump;
    }
}

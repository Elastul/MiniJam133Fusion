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
    }

    private void GetMovementAxis()
    {
        var horizontalMovement = InputController.MovementAxis.x;
        var verticalMovement = InputController.MovementAxis.y;

        

        // if(Physics.Raycast(transform.position, transform.forward * verticalMovement + transform.right * horizontalMovement, out var raycastHit, _wallCheckDistance, _wallLayerMask))
        // {   
        //     Vector3 hitDirection = raycastHit.point - transform.position;
        //     float angle = Vector3.Angle(raycastHit.normal, hitDirection);
        //     if(angle > 70)
        //     {
        //         verticalMovement = 0;
        //         horizontalMovement = 0;
        //     }
            
        // }
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
    private void PreformJump()
    {
        if(!_groundChecker.IsGrounded())
            return;
            
       _playerRigidbody.AddForce((new Vector3(InputController.MovementAxis.x, 0, InputController.MovementAxis.y) + Vector3.up) * _playerStats.JumpForce, ForceMode.Impulse);
    }
    void OnDestroy()
    {
        InputController.JumpButton -= PreformJump;
    }
}

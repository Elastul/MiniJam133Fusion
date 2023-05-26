using UnityEngine;

public class CameraTilt : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    public float tiltSpeed = 2f; 


    public float tiltAngle = 10f;
    public float maxTiltAngle = 45f; 

    private float targetTilt = 0f;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the target tilt based on the input
        targetTilt = Mathf.Clamp(-horizontalInput * tiltAngle, -maxTiltAngle, maxTiltAngle);

        // Apply the tilt smoothly
        float currentTilt = Mathf.LerpAngle(_camera.localEulerAngles.z, targetTilt, tiltSpeed * Time.deltaTime);
        _camera.localEulerAngles = new Vector3(_camera.localEulerAngles.x, _camera.localEulerAngles.y, currentTilt);
    }
}

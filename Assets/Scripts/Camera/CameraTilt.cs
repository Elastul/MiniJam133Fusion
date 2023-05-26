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
        float horizontalInput = InputController.Instance.HorizontalInput;

        targetTilt = Mathf.Clamp(-horizontalInput * tiltAngle, -maxTiltAngle, maxTiltAngle);
        float currentTilt = Mathf.LerpAngle(_camera.localEulerAngles.z, targetTilt, tiltSpeed * Time.deltaTime);

        _camera.localEulerAngles = new Vector3(_camera.localEulerAngles.x, _camera.localEulerAngles.y, currentTilt);
    }
}


using UnityEngine;

public class RotationController : MonoBehaviour
{

    #region Settings
    [Space, Header("Look Settings")]
    public Vector2 Sensitivity = Vector2.zero;
    [SerializeField] private Vector2 smoothAmount = Vector2.zero;
    [SerializeField][Range(-90, 90)] private int lookAngleMin = -90;
    [SerializeField][Range(-90, 90)] private int lookAngleMax = 90;
    #endregion

    private float m_yaw;
    private float m_pitch;

    private float m_desiredYaw;
    private float m_desiredPitch;

    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private Transform _camera;
    void Awake()
    {
        InitValues();
        ChangeCursorState();
    }

    void LateUpdate()
    {
        CalculateRotation();
        SmoothRotation();
        ApplyRotation();
    }
    void InitValues()
    {
        m_yaw = transform.eulerAngles.y;
        m_desiredYaw = m_yaw;
    }

    void CalculateRotation()
    {
        m_desiredYaw += InputController.MouseAxis.x * Sensitivity.x * Time.deltaTime;
        m_desiredPitch -= InputController.MouseAxis.y * Sensitivity.y * Time.deltaTime;

        m_desiredPitch = Mathf.Clamp(m_desiredPitch, lookAngleMin, lookAngleMax);
    }

    void SmoothRotation()
    {
        m_yaw = Mathf.Lerp(m_yaw, m_desiredYaw, smoothAmount.x * Time.deltaTime);
        m_pitch = Mathf.Lerp(m_pitch, m_desiredPitch, smoothAmount.y * Time.deltaTime);
    }

    void ApplyRotation()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_yaw, transform.eulerAngles.z);
        _cameraHolder.eulerAngles = transform.eulerAngles;
        _camera.localEulerAngles = new Vector3(m_pitch, _camera.localEulerAngles.y, _camera.localEulerAngles.z);
    }

    void ChangeCursorState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

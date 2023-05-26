using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform _cameraTransform => transform;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _cameraOffset;
    public float SmoothTime = 0.3F;
    
    private void LateUpdate()
    {
        _cameraTransform.position = Vector3.Lerp(transform.position, _player.position + _cameraOffset, SmoothTime * Time.deltaTime);
    }
}

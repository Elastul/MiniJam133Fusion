using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform _cameraTransform => transform;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _cameraOffset;
    public float SmoothTime = 0.3F;
    private Vector3 _velocity = Vector3.zero;
    
    private void LateUpdate()
    {
        //_cameraTransform.position = _player.position + _cameraOffset;
        _cameraTransform.position = Vector3.SmoothDamp(transform.position, _player.position + _cameraOffset, ref _velocity, SmoothTime * Time.deltaTime);
    }
}

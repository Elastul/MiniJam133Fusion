using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    public float MousePositionX;
    public float MousePositionY; 
    public float HorizontalInput; 
    public float VerticalInput; 

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        MousePositionX = Input.GetAxis("Mouse X");
        MousePositionY = Input.GetAxis("Mouse Y");
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
    }
}

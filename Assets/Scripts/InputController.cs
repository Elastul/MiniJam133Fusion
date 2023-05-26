using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    public float MousePositionX;
    public float MousePositionY; 
    public float HorizontalInput; 
    public float VerticalInput;

    public event Action EButton;
    public event Action QButton;
    public event Action LMBButton;
    public event Action RMBButton; 

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        MousePositionX = Input.GetAxis("Mouse X");
        MousePositionY = Input.GetAxis("Mouse Y");
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.E)) EButton?.Invoke();
        if(Input.GetKeyDown(KeyCode.Q)) QButton?.Invoke();
        if(Input.GetMouseButtonDown(0)) LMBButton?.Invoke();
        if(Input.GetMouseButtonDown(1)) RMBButton?.Invoke();
    }
}

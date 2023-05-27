using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static Vector2 MouseAxis;
    public static Vector2 MovementAxis; 

    public static event Action EButton;
    public static event Action QButton;
    public static event Action LMBButton;
    public static event Action RMBButton;

    void Update()
    {
        MouseAxis =  new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovementAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.E)) EButton?.Invoke();
        if(Input.GetKeyDown(KeyCode.Q)) QButton?.Invoke();
        if(Input.GetMouseButtonDown(0)) LMBButton?.Invoke();
        if(Input.GetMouseButtonDown(1)) RMBButton?.Invoke();
    }
}

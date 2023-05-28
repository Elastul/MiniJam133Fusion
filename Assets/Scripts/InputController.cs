using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static Vector2 MouseAxis;
    public static Vector2 MovementAxis;

    public static bool BlockAxis = false;

    public static event Action EButton;
    public static event Action QButton;
    public static event Action LMBButton;
    public static event Action RMBButton;
    public static event Action JumpButton;
    public static event Action ESCButton;

    void Awake()
    {
        BlockAxis = false;
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape)) ESCButton?.Invoke();
        

        if(BlockAxis)
        {
            MovementAxis = Vector2.zero;
            MouseAxis = Vector2.zero;
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.Space)) JumpButton?.Invoke();
        if(Input.GetKeyDown(KeyCode.E)) EButton?.Invoke();
        if(Input.GetKeyDown(KeyCode.Q)) QButton?.Invoke();
        if(Input.GetMouseButtonDown(0)) LMBButton?.Invoke();
        if(Input.GetMouseButtonDown(1)) RMBButton?.Invoke();

        MouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovementAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }
}

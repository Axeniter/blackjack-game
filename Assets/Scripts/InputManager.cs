using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public event Action<Vector2> RotationInputReceived;

    private InputMap inputMap;
    private KeyboardInput keyboardInput;

    private void Awake()
    {
        inputMap = new InputMap();
        inputMap.Enable();
        InitKeyboardInput();
    }

    private void InitKeyboardInput()
    {
        keyboardInput = new KeyboardInput(inputMap);
        keyboardInput.RotationInputReceived += OnRotationInputReceived;
    }

    private void OnRotationInputReceived(Vector2 delta)
    {
        RotationInputReceived?.Invoke(delta);
    }
}

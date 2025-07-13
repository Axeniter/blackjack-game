using System;
using UnityEngine;

public class KeyboardInput
{
    public event Action<Vector2> RotationInputReceived;

    public KeyboardInput(InputMap inputMap)
    {
        inputMap.Keyboard.MouseDelta.performed += context =>
        {
            var mouseDelta = context.ReadValue<Vector2>();
            RotationInputReceived?.Invoke(mouseDelta);
        };
    }
}

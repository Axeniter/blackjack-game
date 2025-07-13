using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [SerializeField] private Transform rotationTarget;
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float minHorizontal;
    [SerializeField] private float minVertical;
    [SerializeField] private float maxHorizontal;
    [SerializeField] private float maxVertical;

    private float horizontal = 0f;
    private float vertical = 0f;

    private void Start()
    {
        inputManager.RotationInputReceived += OnRotationInputReceived;
    }

    private void OnDestroy()
    {
        inputManager.RotationInputReceived -= OnRotationInputReceived;
    }

    private void OnRotationInputReceived(Vector2 delta)
    {
        var dt = Time.deltaTime;
        vertical -= sensitivity * dt * delta.y;
        horizontal += sensitivity * dt * delta.x;

        vertical = Mathf.Clamp(vertical, minVertical, maxVertical);
        horizontal = Mathf.Clamp(horizontal, minHorizontal, maxHorizontal);

        rotationTarget.eulerAngles = new Vector3(vertical, horizontal, 0);
    }
}
using UnityEngine;

public class InputService : IInputService
{
    public float GetVerticalInput() =>
        Input.GetAxis("Vertical");
    public float GetHorizontalInput() =>
        Input.GetAxis("Horizontal");
    public bool GetBrakeInput () =>
        Input.GetKey(KeyCode.Space);
}

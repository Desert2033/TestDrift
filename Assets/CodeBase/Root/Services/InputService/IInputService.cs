public interface IInputService : IService
{
    bool GetBrakeInput();
    float GetHorizontalInput();
    float GetVerticalInput();
}
using UnityEngine;

public class InputManager : SingleTon<InputManager>
{
    Vector2 joystickInput;
    public bool isActionPressed {  get; private set; }
    public bool isDashPressed {  get; private set; }
    public bool isPlantPressed { get; private set; }
    private void OnEnable()
    {
        Joystick.OnJoystickMove += SetJoystickInput;
        Joystick.OnJoystickRelease += ResetJoystickInput;
        UIActionInput.onTouch += IsPerformingAction;
        UIDashInput.onDashTouch += IsDashPressing;
        UIPlantInput.onPlantTouch += IsPlantPressing;
    }

    private void OnDisable()
    {
        Joystick.OnJoystickMove -= SetJoystickInput;
        Joystick.OnJoystickRelease -= ResetJoystickInput;
        UIActionInput.onTouch -= IsPerformingAction;
        UIDashInput.onDashTouch -= IsDashPressing;
        UIPlantInput.onPlantTouch -= IsPlantPressing;
    }

    private void SetJoystickInput(Vector2 input)
    {
        joystickInput = input;
    }

    private void ResetJoystickInput()
    {
        joystickInput = Vector2.zero;
    }

    public Vector2 GetJoystickInput()
    {
        return joystickInput;
    }

    public void IsDashPressing(bool val)
    {
        isDashPressed = val;
    }
    public void IsPerformingAction(bool val)
    {
        isActionPressed = val;
    }
    public void IsPlantPressing(bool val)
    {
        isPlantPressed = val;
    }
}

using UnityEngine;

public class MobileInputService : InputService
{
    private Joystick _joystick;

    public MobileInputService(Joystick joystick) 
    { 
        _joystick = joystick;
    }

    public override Vector2 GetDirection() => _joystick.Direction;
}

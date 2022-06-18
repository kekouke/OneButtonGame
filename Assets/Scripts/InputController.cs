using UnityEngine;

public static class InputController
{
    public static bool WasKeyPressed(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public static bool WasKeyReleased(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }

    public static bool IsKeyPressed(KeyCode key)
    {
        return Input.GetKey(key);
    }
}

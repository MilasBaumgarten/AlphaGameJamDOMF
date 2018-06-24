using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public UnityEvent OnMonitorButtonPressed;
    public UnityEvent OnFloppyDriveButtonPressed;
    public UnityEvent OnDiskDriveButtonPressed;
    public UnityEvent OnDesktopPowerButtonPressed;
    public UnityEvent OnMouseButtonPressed;
    public UnityEvent OnMonitorClicked;
    public UnityEvent OnGenericInteracted;
    public UnityEvent OnKeyBoardKeyPressed;

    private KeyCode key;

    public void HandleMonitorButton()
    {
        OnMonitorButtonPressed.Invoke();
    }

    public void HandleFloppyDriveButton()
    {
        OnFloppyDriveButtonPressed.Invoke();
    }

    public void HandleDiskDriveButton()
    {
        OnDiskDriveButtonPressed.Invoke();
    }

    public void HandleDesktopPowerButton()
    {
        OnDesktopPowerButtonPressed.Invoke();
    }

    public void HandleMouseButton()
    {
        OnMouseButtonPressed.Invoke();
    }

    public void HandleMonitorScreen()
    {
        OnMonitorClicked.Invoke();
    }

    public void HandleKeyboardKey(Interaction x)
    {
        key = x.key;
        OnKeyBoardKeyPressed.Invoke();
    }

    public void HandleGeneric()
    {
        OnGenericInteracted.Invoke();
    }
}

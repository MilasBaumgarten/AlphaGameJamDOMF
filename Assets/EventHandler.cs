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

    public void HandleInteraction(Interaction obj)
    {
        switch (obj.type)
        {
            case ("KeyBoardKey"):
                {
                    HandleKeyBoardKey(obj.key);
                    break;
                }
            case ("MonitorButton"):
                {
                    HandleMonitorButton();
                    break;
                }
            case ("FloppyDriveButton"):
                {
                    HandleFloppyDriveButton();
                    break;
                }
            case ("DiskDriveButton"):
                {
                    HandleDiskDriveButton();
                    break;
                }
            case ("DesktopPowerButton"):
                {
                    HandleDesktopPowerButton();
                    break;
                }
            case ("MouseButton"):
                {
                    HandleMouseButton();
                    break;
                }
            case ("MonitorScreen"):
                {
                    HandleMonitorScreen();
                    break;
                }
            case ("Generic"):
                {
                    HandleGeneric();
                    break;
                }
            default:
                {
                    Debug.LogError(obj.name + " Has no type!");
                    break;
                }
        }
    }

    void HandleKeyBoardKey(KeyCode key)
    {
        switch (key)
        {
            default:
                {
                    Debug.Log("The Button " + key.ToString() + " has been pressed.");
                    break;
                }
        }
    }

    void HandleMonitorButton()
    {
        OnMonitorButtonPressed.Invoke();
    }

    void HandleFloppyDriveButton()
    {
        OnFloppyDriveButtonPressed.Invoke();
    }

    void HandleDiskDriveButton()
    {
        OnDiskDriveButtonPressed.Invoke();
    }

    void HandleDesktopPowerButton()
    {
        OnDesktopPowerButtonPressed.Invoke();
    }

    void HandleMouseButton()
    {
        OnMouseButtonPressed.Invoke();
    }

    void HandleMonitorScreen()
    {
        OnMonitorClicked.Invoke();
    }

    void HandleGeneric()
    {
        OnGenericInteracted.Invoke();
    }
}

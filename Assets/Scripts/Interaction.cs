using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour {

    public UnityEvent OnButtonPressed;
    public KeyCode key;

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnButtonPressed.Invoke();
        }
    }
}

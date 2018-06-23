using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour {

    public UnityEvent OnButtonPressed;
    public string type = "";
    public KeyCode key;

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("button pressed");
            OnButtonPressed.Invoke();
        }
    }
}

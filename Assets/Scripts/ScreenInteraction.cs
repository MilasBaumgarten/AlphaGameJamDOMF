using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenInteraction : MonoBehaviour
{

    public UnityEvent OnInteracted;
    public bool cursorIsOnTop = false;


    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Cursor")
        {
            cursorIsOnTop = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Cursor")
        {
            cursorIsOnTop = false;
        }
    }

    public void HandleScreenInteraction()
    {
        if (cursorIsOnTop)
        {
            OnInteracted.Invoke();
            Debug.Log("ScreenButton pressed");
        }
    }
}

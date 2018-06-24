using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{

    public UnityEvent OnInteraction;
    public GameObject puzzlePiece;
    public KeyCode key;
    public bool locked = true;

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&!locked)
        {
            Interact();
        }
    }

    public void Unlock(GameObject puzzlePiece)
    {
        if (puzzlePiece == this.puzzlePiece && this.puzzlePiece != null)
        {
            if (puzzlePiece.GetComponent<MoveObject>() != null)
            {
                locked = !puzzlePiece.GetComponent<MoveObject>().mouseDragging;
            }
            else
            {
                locked = false;
            }   
        }
    }

    void Interact()
    {
        OnInteraction.Invoke();
    }
}

using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Rigidbody rb;
    Camera mainCam;


    bool mouseDragging = false;
    bool mouseOverObject = false;

    public ObjectsSO objects;

    [Header("Type of Object")]
    public ObjectType objectType = ObjectType.MOUSE;
    public float moveBounds;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private GameObject cursor;

    public enum ObjectType
    {
        FREE,
        MOUSE
    };

    // stuff einstellen
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        startPosition = transform.position;
        startRotation = transform.rotation;

        cursor = GameObject.Find("Cursor");
    }

    private void Update()
    {
        // Objekt halten/ loslassen
        if (mouseOverObject || mouseDragging)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                mouseDragging = !mouseDragging;
                
                if (mouseDragging)
                {
                    rb.useGravity = false;
                    gameObject.layer = Physics.IgnoreRaycastLayer;
                }
                else
                {
                    rb.useGravity = true;
                    gameObject.layer = 1 << LayerMask.NameToLayer("Default");
                }
            }
        }

        // objekt wird gehalten
        if (mouseDragging)
        {
            RaycastHit hit;
            Vector3 mousePos;
            Ray mouseToWorldRay = mainCam.ScreenPointToRay(Input.mousePosition);

            // berechen wohin Object bewegt werden soll
            if (Physics.Raycast(mouseToWorldRay, out hit, objects.maxDist, objects.collisionLayer))
            {
                mousePos = hit.point + Vector3.up * objects.raiseHeight;
            }
            else
            {
                mousePos = mouseToWorldRay.direction.normalized * objects.maxDist /2 + mainCam.transform.position + Vector3.up * objects.raiseHeight;
            }

            // bewege Objekt abhängig vom Typ
            switch (objectType)
            {
                case (ObjectType.FREE):
                    rb.velocity = (mousePos - transform.position) * objects.followSpeed;
                    break;
                case (ObjectType.MOUSE):
                    // bewege Maus nur auf Oberfläche
                    if (hit.point != Vector3.zero)
                    {
                        // bewege Maus
                        Vector3 dir = Vector3.ProjectOnPlane(mousePos - startPosition, Vector3.up);
                        dir.x = Mathf.Clamp(dir.x, -moveBounds, moveBounds);
                        dir.z = Mathf.Clamp(dir.z, -moveBounds, moveBounds);
                        transform.position = dir + startPosition;
                        // bewege Cursor
                        cursor.transform.localPosition = new Vector3((dir.x/moveBounds) * (objects.canvasSize.x/2), (dir.z/moveBounds) * (objects.canvasSize.y/2), 0);
                    }
                    break;
            }
        }
    }

    private void OnMouseEnter()
    {
        mouseOverObject = true;
    }

    private void OnMouseExit()
    {
        mouseOverObject = false;
    }

    public void Reset()
    {
        Debug.Log(this.gameObject.name + " RESET");
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
    }
}
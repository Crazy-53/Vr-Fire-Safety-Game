using Mono.Cecil.Cil;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LidGrab : MonoBehaviour
{
    public Transform grabPoint; // where the lid will attach when grabbed
    public Transform snapPoint; // where the lid will attach when grabbed
    public KeyCode grabKey = KeyCode.K;

    private bool inGrabArea = false;
    private bool isGrabbed = false;
    private Transform originalParent;
    private Rigidbody rb;
    public Object originalPosition;
    public Object originalRotation;

    void Start()
    {
        originalParent = transform.parent;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (inGrabArea && Input.GetKeyDown(grabKey))
        {
            Debug.Log("key down");

            if (!isGrabbed)
                Grab();
            else
                Release();
        }
    }

    void Grab()
    {
       
        isGrabbed = true;
        rb.isKinematic = true; // disable physics while holding
        
        transform.position = grabPoint.position;
        //transform.rotation = grabPoint.rotation;
        transform.SetParent(grabPoint);

    }
    void PlaceOnPan()
    {
        // Find pan snap point dynamically if needed
        GameObject snapObj = GameObject.FindGameObjectWithTag("PanSnapPoint");
        if (snapObj != null)
            snapPoint = snapObj.transform;
        rb.isKinematic = true;  // disable physics while snapping

        isGrabbed = false;
        transform.position = snapPoint.position;


        //transform.rotation = snapPoint.rotation;
        // Parent to snap point without keeping world position
        transform.SetParent(snapPoint);
        transform.SetParent(null);
        //transform.localEulerAngles = new Vector3(1.652f, -388.6f, 90.801f);
        //transform.localScale = new Vector3(0.3f, 2.83f, 2.83f);
        rb.isKinematic = false;  // disable physics while snapping




        Debug.Log("Lid snapped on pan with custom rotation & scale");
    }

    void Release()
    {
       

        //isGrabbed = false;
        //rb.isKinematic = false;
        PlaceOnPan();


       
        //transform.SetParent(originalParent);
        //transform.position = snapPoint.position;
        //transform.rotation = snapPoint.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            inGrabArea = true;
            Debug.Log("Lid entered grab area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inGrabArea = false;
            Debug.Log("Lid left grab area");
        }
    }
}

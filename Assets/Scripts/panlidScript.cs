using UnityEngine;

public class LidGrab : MonoBehaviour
{
    public Transform grabPoint; // where the lid will attach when grabbed
    public KeyCode grabKey = KeyCode.K; // key to grab/release

    private bool playerNearby = false;
    private bool isGrabbed = false;
    private Transform originalParent;
    private Rigidbody rb;

    void Start()
    {
        originalParent = transform.parent;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(grabKey))
        {
            if (!isGrabbed)
            {
                Grab();
            }
            else
            {
                Release();
            }
        }
    }

    void Grab()
    {
        isGrabbed = true;
        rb.isKinematic = true; // disable physics while holding
        transform.position = grabPoint.position;
        transform.rotation = grabPoint.rotation;
        transform.SetParent(grabPoint);
    }

    void Release()
    {
        isGrabbed = false;
        rb.isKinematic = false;
        transform.SetParent(originalParent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}

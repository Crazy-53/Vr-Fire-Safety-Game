using UnityEngine;

public class pickup : MonoBehaviour
{
    public float pickupRange = 3f;        // مدى الالتقاط
    public Transform holdPoint;           // المكان اللي يمسك فيه الحاجة (مثلاً empty object في إيده)
    private GameObject heldObject;        // الحاجة اللي ماسكها
    public float moveSpeed = 10f;         // سرعة الحركة لتثبيت العنصر في الإيد

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                Drop();
            }
        }

        // لو ماسك حاجة، خلّيها تفضل تتبع الإيد
        if (heldObject != null)
        {
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, holdPoint.position, Time.deltaTime * moveSpeed);
            heldObject.transform.rotation = Quaternion.Lerp(heldObject.transform.rotation, holdPoint.rotation, Time.deltaTime * moveSpeed);
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().useGravity = false;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.transform.SetParent(holdPoint);
            }
        }
    }

    void Drop()
    {
        heldObject.GetComponent<Rigidbody>().useGravity = true;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.transform.SetParent(null);
        heldObject = null;
    }
}
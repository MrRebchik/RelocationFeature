using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public Camera playerCamera;
    public Transform handTransform;
    public float pickUpDistance = 3f;

    private GameObject currentObj;
    private bool canPickUp = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUp();
        if (Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    void PickUp()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, pickUpDistance))
        {
            if (hit.transform.CompareTag("Pickable"))
            {
                if (!canPickUp) Drop();

                currentObj = hit.transform.gameObject;
                Rigidbody rb = currentObj.GetComponent<Rigidbody>();
                Collider col = currentObj.GetComponent<Collider>();

                if (rb && col)
                {
                    rb.isKinematic = true;
                    col.isTrigger = true;
                    currentObj.transform.SetParent(handTransform);
                    currentObj.transform.localPosition = Vector3.zero;
                    currentObj.transform.localRotation = Quaternion.identity;
                    canPickUp = false;
                }
            }
        }
    }

    void Drop()
    {
        if (currentObj)
        {
            currentObj.transform.SetParent(null);
            Rigidbody rb = currentObj.GetComponent<Rigidbody>();
            Collider col = currentObj.GetComponent<Collider>();

            if (rb && col)
            {
                rb.isKinematic = false;
                col.isTrigger = false;
            }

            canPickUp = true;
            currentObj = null;
        }
    }
}
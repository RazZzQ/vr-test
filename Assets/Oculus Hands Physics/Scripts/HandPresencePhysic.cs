using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysic : MonoBehaviour
{
    public Transform target;
    Rigidbody rb;
    public Renderer nonPhysicHand;
    public float showNonPhysiclHandDistance = 0.05f;
    Collider[] handCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handCollider = GetComponentsInChildren<Collider>();
    }
    public void EnabledHandCollider()
    {
        foreach (Collider collider in handCollider)
        {
            collider.enabled = true;
        }
    }
    public void DisabledHandCollider()
    {
        foreach (Collider collider in handCollider)
        {
            collider.enabled = false;
        }
    }
    private void LateUpdate()
    {
        float distance = (target.position - transform.position).magnitude;
        nonPhysicHand.enabled = (distance > showNonPhysiclHandDistance);

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        //Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        //rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        //Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
        //rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);

        transform.rotation = target.rotation;

    }
}

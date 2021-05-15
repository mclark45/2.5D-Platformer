using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Moving Box"))
        {
            float distance = Vector3.Distance(this.transform.position, other.transform.position);

            if (distance < 0.1f)
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                MeshRenderer pressurePadColor = GetComponentInChildren<MeshRenderer>();

                if (rb == null)
                {
                    Debug.LogError("Rigid Body is null");
                }

                if (pressurePadColor == null)
                {
                    Debug.LogError("Mesh Renderer is Null");
                }

                rb.isKinematic = true;
                pressurePadColor.material.color = Color.green;
                Destroy(this);
            }
        }
    }
}

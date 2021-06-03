using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    [SerializeField] private Transform _handPosition, _standPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeChecker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.LedgeGrab(_handPosition, this);
            }
        }
    }

    public Transform GetStandPos()
    {
        return _standPosition;
    }
}

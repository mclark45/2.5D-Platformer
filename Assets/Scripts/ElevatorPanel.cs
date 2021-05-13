using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _callButtonMaterial;
    [SerializeField]
    private int _coinsToCollect = 8;
    private Elevator _elevator;
    private bool _elevatorCalled = false;

    private void Start()
    {
        _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();

        if (_elevator == null)
        {
            Debug.LogError("Elevator is Null");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Input.GetKeyDown(KeyCode.E) && other.GetComponent<Player>().CoinCount() >= _coinsToCollect)
        {
            if (_elevatorCalled == false)
            {
                _callButtonMaterial.material.color = Color.green;
                _elevatorCalled = true;
            }
            else if (_elevatorCalled == true)
            {
                _callButtonMaterial.material.color = Color.red;
                _elevatorCalled = false;
            }
            
            _elevator.CallElevator();
        }
    }
}

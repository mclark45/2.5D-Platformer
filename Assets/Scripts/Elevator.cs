using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Transform _pointOne, _pointTwo;
    [SerializeField]
    private float _speed = 1.0f;
    private bool _goingDown = false;

    private void FixedUpdate()
    {
        if (_goingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointTwo.position, _speed * Time.deltaTime);
        }
        else if (_goingDown == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointOne.position, _speed * Time.deltaTime);
        }
    }

    public void CallElevator()
    {
        _goingDown = !_goingDown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}

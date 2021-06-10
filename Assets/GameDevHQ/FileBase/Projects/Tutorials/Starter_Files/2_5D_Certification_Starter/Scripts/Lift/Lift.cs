using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Transform _waypointOne;
    [SerializeField] private Transform _waypointTwo;
    [SerializeField] private float _speed = 5.0f;
    private bool _isGoingUp = false;

    private void FixedUpdate()
    {
        ElevatorMoving();
        StartCoroutine(MoveElevator());
    }

    private void ElevatorMoving()
    {
        if (transform.position == _waypointOne.position)
        {
            _isGoingUp = false;
        }
        else if (transform.position == _waypointTwo.position)
        {
            _isGoingUp = true;
        }
    }

    private void Up()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypointOne.position, _speed * Time.deltaTime);
    }

    private void Down()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypointTwo.position, _speed * Time.deltaTime);
    }

    IEnumerator MoveElevator()
    {
        Debug.Log("in coroutine");
        if (_isGoingUp == false)
        {
            yield return new WaitForSeconds(5.0f);
            Down();
        }
        else if (_isGoingUp == true)
        {
            yield return new WaitForSeconds(5.0f);
            Up();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _wayPointOne, _wayPointTwo;
    [SerializeField] private float _speed = 1.0f;
    private bool _changeDirections = false;
    
    void FixedUpdate()
    {
        MovingPlatforms();
    }

    private void MovingPlatforms()
    {
        if (_changeDirections == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _wayPointTwo.position, _speed * Time.deltaTime);
        }
        else if (_changeDirections == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _wayPointOne.position, _speed * Time.deltaTime);
        }

        if (transform.position == _wayPointOne.position)
        {
            _changeDirections = false;
        }
        else if (transform.position == _wayPointTwo.position)
        {
            _changeDirections = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}

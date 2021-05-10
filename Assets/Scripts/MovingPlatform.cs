using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA, _pointB;
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _amplitude = 3.5f;
    [SerializeField]
    private float _frequency = 2f;
    private float _xOffSet = 30f;
    private bool _changedirection = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlatforms();
    }

    public void MovePlatforms()
    {
        if (_changedirection == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
        }
        else if (_changedirection == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);
        }

        if (transform.position == _pointA.position)
        {
            _changedirection = false;
        }
        else if (transform.position == _pointB.position)
        {
            _changedirection = true;
        }

        /*float x = Mathf.Cos(Time.time) * _amplitude * _frequency;
        float y = transform.position.y;
        float z = transform.position.z;

        transform.position = new Vector3(x + _xOffSet, y, z);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is marked as Player on Enter");
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is marked as Player on Exit");
            other.transform.SetParent(null);
        }
    }
}

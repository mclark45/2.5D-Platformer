using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Vector3 _cameraPositon;

    private void Start()
    {
        _cameraPositon = transform.position - _player.transform.position;
    }

    void Update()
    {
        transform.position = _player.position + _cameraPositon;
    }
}

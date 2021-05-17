using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight = 5.0f;
    private float _playersYAxisVelocity;
    private CharacterController _controller;

    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("Character Controller is Null");
        }
    }

    
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        Vector3 playerMovement = new Vector3(0, 0, horizontalMovement);
        Vector3 playerVelocity = playerMovement * _speed;

        if (_controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                _playersYAxisVelocity = _jumpHeight;
            }
        }
        else
        {
            _playersYAxisVelocity -= _gravity;
        }

        playerVelocity.y += _playersYAxisVelocity;

        _controller.Move(playerVelocity * Time.deltaTime);
    }
}

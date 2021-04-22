﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _playerSpeed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _playerJumpHeight = 15.0f;
    private float _playersYAxisVelocity;
    private bool _playerHasDoubleJumped = false;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 playerMovement = new Vector3(horizontalInput, 0, 0);
        Vector3 playerVelocity = playerMovement * _playerSpeed;

        if (_controller.isGrounded == true)
        {
            _playerHasDoubleJumped = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playersYAxisVelocity += _playerJumpHeight;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _playerHasDoubleJumped == false)
            {
                _playersYAxisVelocity = (playerVelocity.y + _playerJumpHeight);
                _playerHasDoubleJumped = true;
            }
            _playersYAxisVelocity -= _gravity;
        }

        playerVelocity.y = _playersYAxisVelocity;

        _controller.Move(playerVelocity * Time.deltaTime);
    }
}
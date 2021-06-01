using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight = 5.0f;
    private bool _jumping = false;
    private float _playersYAxisVelocity;
    private CharacterController _controller;
    private Animator _playerAnim;

    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _playerAnim = GetComponentInChildren<Animator>();

        if (_controller == null)
        {
            Debug.LogError("Character Controller is Null");
        }

        if (_playerAnim == null)
        {
            Debug.LogError("Player Animator controller is Null");
        }
    }

    
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        Vector3 playerMovement = new Vector3(0, 0, horizontalMovement);
        Vector3 playerVelocity = playerMovement * _speed;
        _playerAnim.SetFloat("Speed", Mathf.Abs(horizontalMovement));

       if (horizontalMovement != 0)
        {
            Vector3 playerDirection = transform.localEulerAngles;
            playerDirection.y = playerMovement.z > 0 ? 0 : 180;
            transform.localEulerAngles = playerDirection;
        }

        if (_controller.isGrounded == true)
        {
            if (_jumping == true)
            {
                _jumping = false;
                _playerAnim.SetBool("Jump", _jumping);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playersYAxisVelocity = _jumpHeight;
                _jumping = true;
                _playerAnim.SetBool("Jump", _jumping);
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

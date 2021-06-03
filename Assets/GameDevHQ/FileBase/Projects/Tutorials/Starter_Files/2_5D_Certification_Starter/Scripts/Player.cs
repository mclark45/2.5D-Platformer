using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight = 5.0f;
    private bool _jumping = false;
    private bool _ledgeGrab = false;
    private bool _isFalling = false;
    private float _playersYAxisVelocity;
    private CharacterController _controller;
    private Animator _playerAnim;
    private LedgeGrab _activeLedge;

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
        ClimbUp();
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
            _isFalling = false;

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
        else if (_controller.isGrounded == false)
        {
            _playersYAxisVelocity -= _gravity;
            _isFalling = true;
        }

        _playerAnim.SetBool("Falling", _isFalling);

        playerVelocity.y += _playersYAxisVelocity;

        _controller.Move(playerVelocity * Time.deltaTime);
    }

    private void ClimbUp()
    {
        if (_ledgeGrab == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _playerAnim.SetBool("ClimbUp", true);
            }
        }
    }

    public void LedgeGrab(Transform handPosition, LedgeGrab currentLedge)
    {
        _ledgeGrab = true;
        _controller.enabled = false;
        _playerAnim.SetBool("LedgeGrab", true);
        _playerAnim.SetFloat("Speed", 0f);
        _playerAnim.SetBool("Jump", false);
        transform.position = handPosition.position;
        _activeLedge = currentLedge;
    }

    public void OnClimbUpEnd()
    {
        transform.position = _activeLedge.GetStandPos().position;
        _playerAnim.SetBool("ClimbUp", false);
        _playerAnim.SetBool("LedgeGrab", false);
        _controller.enabled = true;
        _ledgeGrab = false;
    }
}

using System.Collections;
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
    private int _coins = 0;
    [SerializeField]
    private int _lives = 3;
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("CharacterController is null");
        }
    }

    void Update()
    {
        PlayerMovement();
    }

    private void FixedUpdate()
    {
        UpdateLives();
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
                _playersYAxisVelocity = _playerJumpHeight;
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

    public void CoinsCollected()
    {
        _coins++;
        UIManager.Instance.CoinsCollectedText(_coins);
    }

    public void UpdateLives()
    {
        if (transform.position.y <= -8f)
        {
            this.transform.position = new Vector3(-1f, 1.58f, 0f);
            Debug.Log("position less than -8");
            _lives--;
            UIManager.Instance.LivesLeft(_lives);
        }

        if (_lives == 0)
        {
            _lives = 3;
            _coins = 0;
            UIManager.Instance.CoinsCollectedText(_coins);
            UIManager.Instance.LivesLeft(_lives);
            GameManager.Instance.ResetCoins();
        }
    }
}
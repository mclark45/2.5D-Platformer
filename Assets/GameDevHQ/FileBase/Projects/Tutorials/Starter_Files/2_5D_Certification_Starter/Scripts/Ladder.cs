using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player _player;
    private CharacterController _playerController;
    private bool _atLadder = false;
    private bool _approachingLadder = false;
    private bool _onLadder = false;
    private Animator _ladderAnimations;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _climbSpeed = 20.0f;
    [SerializeField] private Transform _bottomOfLadder;
    [SerializeField] private Transform _topOfLadder;
    [SerializeField] private float _horizontalOffset = 1.0f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        _ladderAnimations = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();


        if (_player == null)
        {
            Debug.LogError("Player script is Null");
        }

        if (_playerController == null)
        {
            Debug.LogError("Player script is Null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _atLadder = !_atLadder;
        }
    }

    private void LadderMovement()
    {
        if (_atLadder && Input.GetKeyDown(KeyCode.E))
        {
            _ladderAnimations.SetBool("OnLadder", true);
            _onLadder = true;
            _approachingLadder = true;
            _player.enabled = false;
            StartCoroutine(ApproachLadder());
        }

        if (_onLadder)
        {
            float verticalMovement = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(0, verticalMovement, 0);
            Vector3 _velocity = direction * _speed;
            _ladderAnimations.SetFloat("ClimbSpeed", _velocity.y * Time.deltaTime * _climbSpeed);
            _approachingLadder = false;
            _playerController.Move(_velocity * Time.deltaTime);
        }
    }

    IEnumerator ApproachLadder()
    {
        while (_approachingLadder)
        {
            yield return new WaitForEndOfFrame();
            _playerController.Move(Vector3.forward * Time.deltaTime * _horizontalOffset);
        }
    }

    public void TurnLadderOff(bool OnLadder)
    {
        _onLadder = false;
        _ladderAnimations.SetBool("GetOffLadder", true);
    }

    public void EndLadder()
    {
        _atLadder = !_atLadder;
        _ladderAnimations.SetBool("OnLadder", false);
        _ladderAnimations.SetBool("GetOffLadder", false);
        _onLadder = false;
        _player.enabled = true;
        _player.transform.position = _topOfLadder.transform.position;

    }

    private void Update()
    {
        LadderMovement();
    }
}

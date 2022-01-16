using System;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        private Joystick _joystick;
        private CharacterController _characterController;
        private PlayerStats _playerStats;
        private Vector3 _moveDirction;


        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerStats = GetComponent<PlayerStats>();
        }

        private void Start()
        {
            _joystick = FindObjectOfType<Joystick>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _moveDirction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            Debug.Log(_moveDirction);
            _characterController.Move(_moveDirction * _playerStats.MoveSpeed * Time.deltaTime);
        }
    }
}

using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerMovement : MonoBehaviour
    {
        private Joystick _joystick;
        private Rigidbody _rigidbody;
        private PlayerStats _playerStats;
        
        private Vector3 _moveDirction;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerStats = GetComponent<PlayerStats>();
        }

        private void Start()
        {
            _joystick = FindObjectOfType<Joystick>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _moveDirction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            if (_moveDirction.magnitude > 1)
                _moveDirction.Normalize();
            _rigidbody.MovePosition(transform.position + _moveDirction * _playerStats.MoveSpeed * Time.fixedDeltaTime);
            
            if (_moveDirction != Vector3.zero)
                transform.forward = _moveDirction;
        }
    }
}

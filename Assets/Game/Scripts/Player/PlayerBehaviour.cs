using System;
using UnityEngine;

namespace Isometric2DGame.Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField]
        [Min(1.0f)]
        [Tooltip("Player movement speed")]
        private float _playerSpeed;

        [SerializeField]
        [Tooltip("Maximum speed at which the player can move")]
        private float _playerLimitSpeed;

        [SerializeField]
        [Min(2)]
        [Tooltip("Factor that determines how much the player slows down")]
        private int _slowDownFactor;

        private float _currentSpeed;

        [SerializeField]
        private Animator _animator;


        private Rigidbody2D _rB2D;

        private Vector2 _dir;

        private void OnEnable()
        {
            PlayerActions.OnMovementToNorth += MovePlayer;
            PlayerActions.OnMovementToSouth += MovePlayer;
            PlayerActions.OnMovementToEast += MovePlayer;
            PlayerActions.OnMovementToWest += MovePlayer;
            PlayerActions.OnMovementStop += StopPlayer;
        }

        private void OnDisable()
        {
            PlayerActions.OnMovementToNorth -= MovePlayer;
            PlayerActions.OnMovementToSouth -= MovePlayer;
            PlayerActions.OnMovementToEast -= MovePlayer;
            PlayerActions.OnMovementToWest -= MovePlayer;
            PlayerActions.OnMovementStop -= StopPlayer;
        }

        private void Awake()
        {
            _rB2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _currentSpeed = _playerSpeed;
        }

        private Vector2 ConvertToIsometric(Vector2 dir)
        {
            return new Vector2(dir.x - dir.y, (dir.x + dir.y) / 2f);
        }

        private void StopPlayer(Vector2 dir)
        {
            _dir -= ConvertToIsometric(dir);

            if (_dir == Vector2.zero)
            {
                _rB2D.linearVelocity = Vector2.zero;
                _animator.SetFloat("x", 0);
                _animator.SetFloat("y", 0);
            }
        }

        private void MovePlayer(Vector2 dir)
        {
            _dir += ConvertToIsometric(dir);
            if(_rB2D.linearVelocity.magnitude < _playerLimitSpeed)
            {
                _rB2D.linearVelocity = _dir.normalized * _currentSpeed;
                _animator.SetFloat("x", dir.x);
                _animator.SetFloat("y", dir.y);
            }
        }

        public void SlowsDownPlayer()
        {
            _currentSpeed /= _slowDownFactor;
            print("S");
        }

        public void ReturnNormalSpeed()
        {
            _currentSpeed = _playerSpeed;
            print("B");
        }
    }
}

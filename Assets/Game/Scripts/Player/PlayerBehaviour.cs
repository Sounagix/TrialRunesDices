using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Isometric2DGame.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerBehaviour : Entity
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

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private BowBehaviour _bowBehaviour;

        [SerializeField]
        private MeleeBehaviour _meleeBehaviour;


        private float _currentSpeed;

        private Rigidbody2D _rB2D;

        private Vector2 _dir;

        private void OnEnable()
        {
            PlayerActions.OnMovementToNorth += MovePlayer;
            PlayerActions.OnMovementToSouth += MovePlayer;
            PlayerActions.OnMovementToEast += MovePlayer;
            PlayerActions.OnMovementToWest += MovePlayer;
            PlayerActions.OnMovementStop += StopPlayer;
            PlayerActions.OnPlayerShoot += OnPlayerShoot;
            PlayerActions.OnPlayerMeleeAttack += OnPlayerMelee;
        }

 

        private void OnDisable()
        {
            PlayerActions.OnMovementToNorth -= MovePlayer;
            PlayerActions.OnMovementToSouth -= MovePlayer;
            PlayerActions.OnMovementToEast -= MovePlayer;
            PlayerActions.OnMovementToWest -= MovePlayer;
            PlayerActions.OnMovementStop -= StopPlayer;
            PlayerActions.OnPlayerShoot -= OnPlayerShoot;
            PlayerActions.OnPlayerMeleeAttack -= OnPlayerMelee;
        }

        private void Awake()
        {
            _rB2D = GetComponent<Rigidbody2D>();
            _currentHealth = _initHealth;
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
            _animator.SetBool(GeneralData.movementBoolName, false);
            _dir -= ConvertToIsometric(dir);
            _rB2D.linearVelocity = Vector2.zero;
            _animator.SetFloat(GeneralData.xVelAnimName, dir.x);
            _animator.SetFloat(GeneralData.yVelAnimName, dir.y);
            //if (_dir == Vector2.zero)
            //{
            //    _rB2D.linearVelocity = Vector2.zero;
            //    _animator.SetFloat(GeneralData.xVelAnimName, _dir.x);
            //    _animator.SetFloat(GeneralData.yVelAnimName, _dir.y);
            //}
        }

        private void MovePlayer(Vector2 dir)
        {
            _dir += ConvertToIsometric(dir);
            if(_rB2D.linearVelocity.magnitude < _playerLimitSpeed)
            {
                _rB2D.linearVelocity = _dir.normalized * _currentSpeed;
                _animator.SetFloat(GeneralData.xVelAnimName, dir.x);
                _animator.SetFloat(GeneralData.yVelAnimName, dir.y);
                _animator.SetBool(GeneralData.movementBoolName, true);
            }
        }

        public void SlowsDownPlayer()
        {
            _currentSpeed /= _slowDownFactor;
        }

        public void ReturnNormalSpeed()
        {
            _currentSpeed = _playerSpeed;
        }

        private void OnPlayerShoot()
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector2 dir = (new Vector2(worldMousePos.x, worldMousePos.y) - (Vector2)transform.position).normalized;

            _animator.SetInteger("Dir", GetDir(dir));
            _animator.SetTrigger(GeneralData.attackTriggerName);
        }

        private int GetDir(Vector2 dir)
        {
            if (dir.x == 0 && dir.y == 0)
                return 2;
            else if(math.abs(dir.x) > math.abs(dir.y))
            {
                print("WS");
                return dir.x > 0 ? 3 : 2;
            }
            else
            {
                print("NS");
                return dir.y > 0 ? 1 : 0;
            }
        }

        public override void Shoot()
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector2 dir = (new Vector2(worldMousePos.x, worldMousePos.y) - (Vector2)transform.position).normalized;
            _bowBehaviour.Attack(dir);
        }


        public override void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
                print("Reset");
            else
                PlayerActions.OnPlayerReceiveDamage?.Invoke(_currentHealth, _initHealth);
        }

        private void OnPlayerMelee()
        {
            _meleeBehaviour.Attack(_dir);
        }
    }
}

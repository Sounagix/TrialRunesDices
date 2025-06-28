using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Isometric2DGame.Player;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Windows;
using Unity.VisualScripting;

namespace Isometric2DGame.Enemy
{
    public enum ENEMY_STATE: int
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        NULL
    }

    public class Enemy : Entity
    {
        [SerializeField]
        private float _minTimeToStop;

        [SerializeField]
        private float _maxTimeToStop;

        [SerializeField]
        [Tooltip("Points where the enemy is going to patrol")]
        private List<Transform> _patrolPoints = new();

        [SerializeField]
        [Min(0.001f)]
        [Tooltip("Speed used to move")]
        private float _movementSpeed;

        [SerializeField]
        [Min(0.5f)]
        private float _meleeRange;

        [SerializeField]
        [Min(0.5f)]
        private float _meleeCoolDown;

        [SerializeField]
        private float _detectionRange;

        [SerializeField]
        private int _meleeDamage;

        [SerializeField]
        private Animator _animator;

        private int _patrolIndex = -1;

        [SerializeField]
        private ENEMY_STATE _sTATE = ENEMY_STATE.NULL;

        private PlayerBehaviour _enemy;

        private Vector2 _dir = Vector2.zero;

        private Coroutine _attackCoroutine;


        private void Awake()
        {
            CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
            if (circleCollider2D) 
                circleCollider2D.radius = _detectionRange;

            _currentHealth = _initHealth;
        }


        private void Start()
        {
            StartIdle();
        }

        private void StartIdle()
        {
            _sTATE = ENEMY_STATE.IDLE;
            _dir = Vector2.zero;
            _enemy = null;
            _animator.SetFloat(GeneralData.xVelAnimName, 0);
            _animator.SetFloat(GeneralData.yVelAnimName, 0);
            float timeToStartToPatrol = Random.Range(_minTimeToStop, _maxTimeToStop);
            Invoke(nameof(StartPatrolling), timeToStartToPatrol);
        }

        private void StartPatrolling() 
        {
            _sTATE = ENEMY_STATE.PATROL;
            int currentIndex = -1;
            do
            {
                currentIndex = Random.Range(0, _patrolPoints.Count);
            }
            while (_patrolIndex == currentIndex);
            _patrolIndex = currentIndex;
        }
        private void ManagePatrolling()
        {
            float distance = Vector2.Distance(_patrolPoints[_patrolIndex].position, transform.position);
            if (distance > 0.25f)
                SetPosition(_patrolPoints[_patrolIndex]);
            else
                StartIdle();
        }

        private void StartChase()
        {
            _sTATE = ENEMY_STATE.CHASE;
        }

        private void ManageChase()
        {
            if (!_enemy)
                StartIdle();
            else
            {
                float distance = Vector2.Distance(transform.position, _enemy.transform.position);
                if (distance > _meleeRange)
                {
                    SetPosition(_enemy.transform);
                }
                else
                {
                    StartAttack();
                }
            }
        }

        private void StartAttack()
        {
            _animator.SetFloat(GeneralData.xVelAnimName, 0);
            _animator.SetFloat(GeneralData.yVelAnimName, 0);
            _sTATE = ENEMY_STATE.ATTACK;
        }




        private void ManageAttack()
        {
            if (!_enemy)
                StartIdle();
            else
            {
                float distance = Vector2.Distance(transform.position, _enemy.transform.position);
                if (distance > _meleeRange)
                {
                    StartChase();
                }
                else if (_attackCoroutine == null)
                {
                    _attackCoroutine = StartCoroutine(nameof(Attack));
                }
            }
        }

        private IEnumerator Attack()
        {
            if (!_enemy || !_sTATE.Equals(ENEMY_STATE.ATTACK))
            {
                yield return null;
                _attackCoroutine = null;
            }
            if (_animator)
                _animator.SetTrigger(GeneralData.attackTriggerName);
            _enemy.TakeDamage(_meleeDamage);
            yield return new WaitForSecondsRealtime(_meleeCoolDown);
            _attackCoroutine = null;
        }


        private void SetPosition(Transform target)
        {
            _dir = (target.position - transform.position).normalized;
            transform.Translate(_dir * _movementSpeed);
        }


        private void LateUpdate()
        {
            switch (_sTATE)
            {
                case ENEMY_STATE.IDLE:
                    break;
                case ENEMY_STATE.PATROL:
                    ManagePatrolling();
                    break;
                case ENEMY_STATE.CHASE:
                    ManageChase();
                    break;
                case ENEMY_STATE.ATTACK:
                    ManageAttack();
                    break;
                case ENEMY_STATE.NULL:
                    break;
            }

            if (_animator && !_sTATE.Equals(ENEMY_STATE.ATTACK) && _dir.magnitude > 0.1f)
            {
                _animator.SetFloat(GeneralData.xVelAnimName, Round(_dir.x));
                _animator.SetFloat(GeneralData.yVelAnimName, Round(_dir.y));
            }
        }

        private float Round(float value)
        {
            if (value >= 0.5f) return 1.0f;
            if (value <= -0.5f) return -1.0f;
            return 0.0f;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag(GeneralData.playerTag))
            {
                _enemy = collision.GetComponent<PlayerBehaviour>();
                if (_enemy)
                {
                    StartChase();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(GeneralData.playerTag))
            {
                _enemy = null;
                StartIdle();
            }
        }

        public override void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                UiActions.RemoveLifeBar?.Invoke(this);
                Destroy(gameObject);
            }
            else
            {
                UiActions.CreateLifeBar?.Invoke(this);
            }
        }
    }
}

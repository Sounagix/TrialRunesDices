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
        protected float _minTimeToStop;

        [SerializeField]
        protected float _maxTimeToStop;

        protected List<Transform> _patrolPoints = new();

        [SerializeField]
        [Min(0.001f)]
        [Tooltip("Speed used to move")]
        protected float _movementSpeed;

        [SerializeField]
        [Min(0.5f)]
        protected float _attackRange;

        [SerializeField]
        [Min(0.5f)]
        protected float _attackCooldown;

        [SerializeField]
        protected float _detectionRange;

        [SerializeField]
        protected int _attackDamage;

        [SerializeField]
        protected Animator _animator;

        protected int _patrolIndex = -1;

        [SerializeField]
        protected ENEMY_STATE _sTATE = ENEMY_STATE.NULL;

        [SerializeField]
        private GameObject _body;


        protected PlayerBehaviour _enemy;

        protected Vector2 _dir = Vector2.zero;

        protected Coroutine _attackCoroutine;


        private void Awake()
        {
            CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
            if (circleCollider2D) 
                circleCollider2D.radius = _detectionRange;

            _currentHealth = _initHealth;
        }

        public void SetUp(List<Transform> patrolPoints) 
        {
            _patrolPoints = patrolPoints;
        }


        private void Start()
        {
            StartIdle();
        }

        protected virtual void StartIdle()
        {
            _sTATE = ENEMY_STATE.IDLE;
            _dir = Vector2.zero;
            _enemy = null;
            _animator.SetFloat(GeneralData.xVelAnimName, 0);
            _animator.SetFloat(GeneralData.yVelAnimName, 0);
            float timeToStartToPatrol = Random.Range(_minTimeToStop, _maxTimeToStop);
            Invoke(nameof(StartPatrolling), timeToStartToPatrol);
        }

        protected virtual void StartPatrolling() 
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
        protected virtual void ManagePatrolling()
        {
            float distance = Vector2.Distance(_patrolPoints[_patrolIndex].position, transform.position);
            if (distance > 0.25f)
                SetPosition(_patrolPoints[_patrolIndex]);
            else
                StartIdle();
        }

        protected virtual void StartChase()
        {
            _sTATE = ENEMY_STATE.CHASE;
        }

        protected virtual void ManageChase()
        {
            if (!_enemy)
                StartIdle();
            else
            {
                float distance = Vector2.Distance(transform.position, _enemy.transform.position);
                if (distance < _attackRange)
                {
                    StartAttack();
                }
                else if (distance < _detectionRange)
                {
                    SetPosition(_enemy.transform);
                }
            }
        }

        protected virtual void StartAttack()
        {
            _animator.SetFloat(GeneralData.xVelAnimName, 0);
            _animator.SetFloat(GeneralData.yVelAnimName, 0);
            _sTATE = ENEMY_STATE.ATTACK;
        }




        protected virtual void ManageAttack()
        {
            if (!_enemy)
                StartIdle();
            else
            {
                float distance = Vector2.Distance(transform.position, _enemy.transform.position);
                if (distance > _attackRange)
                {
                    StartChase();
                }
                else if (_attackCoroutine == null)
                {
                    _attackCoroutine = StartCoroutine(nameof(Attack));
                }
            }
        }

        protected virtual IEnumerator Attack()
        {
            if (!_enemy || !_sTATE.Equals(ENEMY_STATE.ATTACK))
            {
                yield return null;
                _attackCoroutine = null;
            }
            if (_animator)
                _animator.SetTrigger(GeneralData.attackTriggerName);
            _enemy.TakeDamage(_attackDamage);
            yield return new WaitForSecondsRealtime(_attackCooldown);
            _attackCoroutine = null;
        }


        protected void SetPosition(Transform target)
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

        protected bool OnRange()
        {
            return Vector2.Distance(transform.position, _enemy.transform.position) <= _detectionRange;
        }


        public override void TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                _enemy = null;
                _dir = Vector2.zero;
                UiActions.RemoveLifeBar?.Invoke(this);
                _animator.SetTrigger(GeneralData.deathTriggerName);
            }
            else
            {
                UiActions.CreateLifeBar?.Invoke(this);
            }
        }

        public override void OnDie()
        {
            GameObject body = Instantiate(_body);
            body.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

}

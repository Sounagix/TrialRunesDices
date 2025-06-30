using Isometric2DGame.Enemy;
using UnityEngine;

public class Shark : Enemy
{
    [SerializeField]
    private Charge _charge;

    protected override void ManageChase()
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
                if (_charge.CanUse())
                {
                    _charge.SetUp(_enemy.transform);
                    _charge.ActiveSkill();
                }
                else
                    SetPosition(_enemy.transform);
            }
        }
    }
}

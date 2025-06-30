using System.Collections;
using UnityEngine;

public class MeleeBehaviour : BaseAttack
{
    [SerializeField]
    private int _numOfRC;

    [SerializeField]
    private float _rangeAttack;



    protected override IEnumerator AttackCoroutine()
    {
        float spreadAngle = 60f; 
        float angleStep = spreadAngle / (_numOfRC - 1);
        float startAngle = -spreadAngle / 2f;

        /*
            _dir = (1, 0)  angle = 0° 
            _dir = (0, 1)  angle = 90° 
            _dir = (-1, 0) angle = 180°
            _dir = (0, -1) angle = -90°
         */
        float baseAngle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < _numOfRC; i++)
        {
            float angle = baseAngle + startAngle + i * angleStep;
            Vector2 rayDir = AngleToDirection(angle);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, _rangeAttack, LayerMask.GetMask("Enemy"));

            Debug.DrawRay(transform.position, rayDir * _rangeAttack, Color.yellow, 0.5f);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Entity>().TakeDamage(_damage);
                break;
            }
        }

        yield return new WaitForSecondsRealtime(_coolDown);
        _attackCoroutine = null;
    }

    private Vector2 AngleToDirection(float angleInDegrees)
    {
        float rad = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}

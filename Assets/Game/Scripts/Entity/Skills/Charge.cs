using System.Collections;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Charge : Skill
{
    [SerializeField]
    private float _chargeTimeDuration;

    private Rigidbody2D _rB2D;

    private Coroutine _coroutine;

    public override void SetUp(Transform target = null)
    {
        base.SetUp(target);
        _rB2D = GetComponent<Rigidbody2D>();
    }

    public override void ActiveSkill()
    {
        _initTime = Time.time;
        _coroutine = StartCoroutine(nameof(LerpMovement));
    }

    private IEnumerator LerpMovement()
    {
        float elapsed = 0f;
        Vector2 initPos = transform.position;
        Vector2 endPos = _target.transform.position;

        while (elapsed < _chargeTimeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _chargeTimeDuration;

            Vector2 newPos = Vector2.Lerp(initPos, endPos, t);
            transform.position = newPos;

            yield return null;
        }

        transform.position = endPos;
        _coroutine = null;
        yield return null;
    }
}

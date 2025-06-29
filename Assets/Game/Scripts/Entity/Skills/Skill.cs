using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    protected float _cooldown;

    [SerializeField]
    protected bool _requiereTarget;

    protected Transform _target;

    protected float _initTime = 0f;

    public abstract void ActiveSkill();

    public virtual bool CanUse()
    {
        return Time.time - _initTime > _cooldown;
    }

    public virtual void SetUp(Transform target = null)
    {
        if (_requiereTarget && target == null)
            Debug.LogError(gameObject.name + " the skill has no target");
        _target = target;
    }
}

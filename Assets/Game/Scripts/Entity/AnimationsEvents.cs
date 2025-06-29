using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    private Entity _entity;

    private void Awake()
    {
        _entity = GetComponentInParent<Entity>();
        if (!_entity)
            Debug.LogError(name + " Must have an associated entity");
    }


    public void OnEntityDie()
    {
        _entity.OnDie();
    }

    public void Shoot()
    {
        _entity.Shoot();
    }
}

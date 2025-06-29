using System;
using UnityEngine;

public enum PTC_TYPE
{
    HURT,

}

public static class ParticleActions
{
    public static Action<Vector2, Quaternion, PTC_TYPE> CreateHurtPTC;
}

public class ParticleSystemManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _hurtPTC;

    private void OnEnable()
    {
        ParticleActions.CreateHurtPTC += CreatePTC;
    }

    private void OnDisable()
    {
        ParticleActions.CreateHurtPTC -= CreatePTC;
    }

    private void CreatePTC(Vector2 pos, Quaternion q, PTC_TYPE tYPE)
    {
        switch (tYPE)
        {
            case PTC_TYPE.HURT:
                ParticleSystem particleSystem = Instantiate(_hurtPTC);
                particleSystem.transform.position = pos;
                particleSystem.transform.rotation = Quaternion.Euler(q.eulerAngles.z,-90,0.0f);
                Destroy(particleSystem.gameObject, 1.0f);
                break;
        }
    }
}
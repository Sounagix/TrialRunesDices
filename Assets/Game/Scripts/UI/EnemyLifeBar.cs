using System.Collections;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField]
    private float _yGap;

    [SerializeField]
    private Scrollbar _scrollbar;

    private float _activeTime;

    private Entity _target;

    private RectTransform _rectTransform;

    private Coroutine _disableBar = null;

    public void SetUp(Entity target, float activeTime)
    {
        _activeTime = activeTime;
        _target = target;
        _rectTransform = GetComponent<RectTransform>();
        float value = _target.GetCurrentHealth() / _target.GetMaxLife();
        _scrollbar.size = value;
        _disableBar = StartCoroutine(DisableBar());
    }

    public void UpdateLifeBar()
    {
        gameObject.SetActive(true);
        StopCoroutine(nameof(DisableBar));
        _disableBar = null;

        float value = _target.GetCurrentHealth() / _target.GetMaxLife();
        _scrollbar.size = value;
        _disableBar = StartCoroutine(DisableBar());
    }

    private void LateUpdate()
    {
        if (gameObject.activeInHierarchy && _target)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(_target.transform.position);
            _rectTransform.position = new Vector3(screenPos.x, screenPos.y + _yGap, 0);
        }
    }

    private IEnumerator DisableBar()
    {
        yield return new WaitForSecondsRealtime(_activeTime);
        gameObject.SetActive(false);
        _disableBar = null;
    }
}

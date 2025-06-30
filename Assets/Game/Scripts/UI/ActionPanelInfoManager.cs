using System.Collections.Generic;
using UnityEngine;

public class ActionPanelInfoManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _actionPanel;

    [SerializeField]
    private float yGap;

    private void OnEnable()
    {
        UiActions.AddPanelInfo += CreatePanel;
        UiActions.RemovePanelInfo += DestroyPanel;
    }
    
    public void CreatePanel(ItemOnGame iOG)
    {
        _actionPanel.gameObject.SetActive(true);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(iOG.transform.position);
        RectTransform rT = _actionPanel.GetComponent<RectTransform>();
        rT.transform.position = new Vector3(screenPos.x, screenPos.y + yGap, screenPos.z);
    }
    
    public void DestroyPanel(ItemOnGame iOG)
    {
        _actionPanel.gameObject.SetActive(false);
    }
}

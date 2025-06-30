using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ActionPanelInfoManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _actionPanel;

    [SerializeField]
    private float yGap;

    [SerializeField]
    private string _itemText;

    [SerializeField]
    private string _npcText;


    private void OnEnable()
    {
        UiActions.AddPanelInfo += CreatePanel;
        UiActions.RemovePanelInfo += DestroyPanel;
        UiActions.AddPanelNPC += CreateNPCPanel;
        UiActions.RemovePanelNPC += DestroyNPCPanel;
    }

    private void OnDisable()
    {
        UiActions.AddPanelInfo -= CreatePanel;
        UiActions.RemovePanelInfo -= DestroyPanel;
        UiActions.AddPanelNPC -= CreateNPCPanel;
        UiActions.RemovePanelNPC -= DestroyNPCPanel;

    }

    public void CreateNPCPanel(NPC nPC)
    {
        _actionPanel.gameObject.SetActive(true);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(nPC.transform.position);
        RectTransform rT = _actionPanel.GetComponent<RectTransform>();
        rT.transform.position = new Vector3(screenPos.x, screenPos.y + yGap, screenPos.z);
        _actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = _npcText;
    }

    public void DestroyNPCPanel(NPC nPC)
    {
        _actionPanel.gameObject.SetActive(false);
    }


    public void CreatePanel(ItemOnGame iOG)
    {
        _actionPanel.gameObject.SetActive(true);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(iOG.transform.position);
        RectTransform rT = _actionPanel.GetComponent<RectTransform>();
        rT.transform.position = new Vector3(screenPos.x, screenPos.y + yGap, screenPos.z);
        _actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = _itemText;
    }
    
    public void DestroyPanel(ItemOnGame iOG)
    {
        _actionPanel.gameObject.SetActive(false);
    }
}

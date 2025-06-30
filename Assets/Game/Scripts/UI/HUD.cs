using Isometric2DGame.Player;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Scrollbar _playerScrollbar;

    private void OnEnable()
    {
        PlayerActions.OnPlayerReceiveDamage += UpdateLifebar;
    }

    private void OnDisable()
    {
        PlayerActions.OnPlayerReceiveDamage -= UpdateLifebar;
    }

    private void UpdateLifebar(float currentHP, float maxHP)
    {
       float normalizeHPValue = currentHP / maxHP;
        _playerScrollbar.size = normalizeHPValue;
    }
}

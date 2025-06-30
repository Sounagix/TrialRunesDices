using System;
using UnityEngine;

public static class UiActions
{
    public static Action<Entity> CreateLifeBar;
    public static Action<Entity> RemoveLifeBar;
    public static Action<ItemOnGame> AddPanelInfo;
    public static Action<ItemOnGame> RemovePanelInfo;
    public static Action<NPC> AddPanelNPC;
    public static Action<NPC> RemovePanelNPC;
    public static Action OnOpenInventory;
    public static Action UpdateInventory;
    public static Action<Dialogue> OpenDialogueUI;
    public static Action CloseDialogueUI;
}

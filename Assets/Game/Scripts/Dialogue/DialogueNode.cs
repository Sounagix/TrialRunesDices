using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Node")]

public class DialogueNode : ScriptableObject
{
    public string _npcText;

    public List<DialogueChoice> _choices = new();

}

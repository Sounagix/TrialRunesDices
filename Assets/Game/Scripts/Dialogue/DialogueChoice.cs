using UnityEngine;

[CreateAssetMenu(fileName = "NewAnswer", menuName = "Dialogue System/Answer")]

public class DialogueChoice : ScriptableObject
{
    public string _playerResponse;

    public DialogueNode _nextNode;
}

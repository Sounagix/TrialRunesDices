using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainPanel;

    [SerializeField]
    private TextMeshProUGUI _dialogueText;

    [SerializeField]
    private GameObject _answerOption;

    [SerializeField]
    private Transform _optionPool;

    private DialogueNode _currentNode;

    private void OnEnable()
    {
        UiActions.OpenDialogueUI += ActiveDialogue;
        UiActions.CloseDialogueUI += CloseDialogue; 
    }



    private void OnDisable()
    {
        UiActions.OpenDialogueUI -= ActiveDialogue;
        UiActions.CloseDialogueUI -= CloseDialogue;

    }


    public void ActiveDialogue(Dialogue dialogue)
    {
        _mainPanel.SetActive(true);
        _currentNode = dialogue._startNode;
        SetUpDialogue();
    }

    private void CloseDialogue()
    {
        _currentNode = null;
        _mainPanel.SetActive(false);
        DestroyOptions();
    }

    private void SetUpDialogue()
    {
        _dialogueText.text = _currentNode._npcText;
        ShowAnswers();
    }

    private void ShowAnswers()
    {
        foreach (DialogueChoice dC in _currentNode._choices)
        {
            GameObject currentAnswer = Instantiate(_answerOption, _optionPool);
            currentAnswer.GetComponentInChildren<TextMeshProUGUI>().text = dC._playerResponse;
            currentAnswer.GetComponent<Button>().onClick.AddListener(
                delegate ()
                {
                    _currentNode = dC._nextNode;
                    DestroyOptions();
                    SetUpDialogue();
                });
        }
    }

    private void DestroyOptions()
    {
        foreach (Transform tr in _optionPool)
        {
            Destroy(tr.gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Networking;

public class DialougeManager : MonoBehaviour
{
    public CanvasGroup dialogueObject;
    public Image charIcon;
    public TextMeshProUGUI charName;
    public TextMeshProUGUI dialogueArea;
    DialogueTrigger currentTrigger;
    Queue<DialogueLine> lines = new Queue<DialogueLine>();
    public bool isDialogueActive;
    float typingSpeed = 0.03f;
    [SerializeField]
    AudioClip touchSfx;

    public static DialougeManager Instance { get; private set; }

    private void Start()
    {   
        Instance = this;
        Hide();
    }
    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger)
    {
        isDialogueActive = true;
        currentTrigger = trigger;
        GameStateManager.Instance.ChangeState(GameState.dialogue);
        Show();
        if (lines != null)
            lines.Clear();
        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            lines.Enqueue(line);
        }
        DisplayNextDialogueLine();
    }

    private void Show()
    {
        UiHelper.Toogle(true);
        dialogueObject.gameObject.SetActive(true);
        dialogueObject.DOFade(1, 1f);
    }

    public void DisplayNextDialogueLine()
    {
        SoundManager.Instance.PlayOS(touchSfx);
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialogueLine curLine = lines.Dequeue();
        charIcon.sprite = curLine.character.icon;
        charName.text = curLine.character.charName;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(curLine));
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        Hide();
        GameStateManager.Instance.ChangeState(GameState.normal);
        if (currentTrigger != null)
        {
            currentTrigger.TryActivateFeatrue();
            currentTrigger = null;
        }
    }

    private void Hide()
    {
        dialogueObject.DOFade(0, 1f);
        dialogueObject.DOKill();
        dialogueObject.gameObject.SetActive(false);
        UiHelper.Toogle(false);
    }

    IEnumerator TypeSentence(DialogueLine curLine)
    {
        dialogueArea.text = "";
        foreach (char letter in curLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void playAudio(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}

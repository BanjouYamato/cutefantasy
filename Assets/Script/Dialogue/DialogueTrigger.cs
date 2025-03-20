using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSO dialogueData;
    public void TriggerDialogue()
    {
        DialougeManager.Instance.StartDialogue(dialogueData.dialogue, this);
    }
    public void TryActivateFeatrue()
    {
        NPCFeature feature = GetComponent<NPCFeature>();
        if (feature != null)
        {
            feature.ActivateFeature(dialogueData.featureName);
        }
    }
}


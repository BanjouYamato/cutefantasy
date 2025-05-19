using UnityEngine;
using UnityEngine.UI;

public class UiSaveGame : MonoBehaviour
{
    [SerializeField]
    Button saveGame, noSave;

    [SerializeField]
    GameObject saveGamePanel;
    [SerializeField]
    PlayerStats stats;

    [SerializeField]
    AudioClip saveGamePanelSFX;

    private void Start()
    {
        Observer.Instance.AddToList<bool>(ObserverCostant.SAVE_GAME, Toggle);
        saveGame.onClick.AddListener(SaveGamePerform);
        noSave.onClick.AddListener(() => Toggle(false));
        saveGamePanel.SetActive(false);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveToList<bool>(ObserverCostant.SAVE_GAME, Toggle);
    }
    void Toggle(bool val)
    {   if (val)
            SoundManager.Instance.PlayOS(saveGamePanelSFX);
        else
            SoundManager.Instance.PlayOS();
        UiHelper.Toogle(val);
        saveGamePanel.SetActive(val);
    }
    void SaveGamePerform()
    {
        SoundManager.Instance.PlayOS();
        PlayerControler.instance.PlayerStats.ResetStats(stats.weapon);
        SceneControler.Instance.SaveScene();
        Observer.Instance.Notify(GameConstant.GAME_SAVE);
        Toggle(false);
    }
}

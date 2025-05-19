using UnityEngine;

public class SeedSave : SaveLoadControler
{
    [SerializeField]   
    
    UIPlantInput uIPlantInput;
    public override void OnLoad()
    {
        GameControler.Instance.runTimeData.currentSeed = SaveGameManager.Instance.Load<InventoryItem>(GameConstant.GAME_SAVE);
        Observer.Instance.Notify<Sprite>(ObserverCostant.UI_PLANT_BUTTON, GameControler.Instance.runTimeData.currentSeed.item.Icon);
    }

    public override void OnNewGame()
    {
        GameControler.Instance.runTimeData.currentSeed = null;
        Observer.Instance.Notify(ObserverCostant.UI_PLANT_BUTTON);
    }

    public override void Onsave()
    {
        SaveGameManager.Instance.Save<InventoryItem>(GameConstant.GAME_SAVE,
            GameControler.Instance.runTimeData.currentSeed);
    }
}

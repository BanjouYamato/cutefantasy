public class FarmingAreaSave : SaveLoadControler
{
    public override void OnLoad()
    {
        DugTileList list = SaveGameManager.Instance
            .Load<DugTileList>(GameConstant.FARMTILES_DATA);
        GameControler.Instance.runTimeData.dugTileList = list.tiles;
    }

    public override void OnNewGame()
    {
        GameControler.Instance.runTimeData.dugTileList.Clear();
    }

    public override void Onsave()
    {
        DugTileList list = new DugTileList(GameControler.Instance.runTimeData.dugTileList);
        SaveGameManager.Instance.Save<DugTileList>(GameConstant.FARMTILES_DATA, list);
    }
}

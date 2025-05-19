using System.Collections.Generic;

public class PartnerSave : SaveLoadControler
{
    public override void OnLoad()
    {
        PartnerList data = SaveGameManager.Instance.Load<PartnerList>(GameConstant.GAME_SAVE);
        GameControler.Instance.partnerTraveler = data.partners;
    }

    public override void OnNewGame()
    {
        GameControler.Instance.partnerTraveler.Clear();
    }

    public override void Onsave()
    {
        PartnerList data = new PartnerList(GameControler.Instance.partnerTraveler);
        SaveGameManager.Instance.Save<PartnerList>(GameConstant.GAME_SAVE, data);
    }
}
public struct PartnerList
{
    public List<PartnerStats> partners;

    public PartnerList(List<PartnerStats> partners)
    {
        this.partners = partners;
    }
}

using UnityEngine;

public class PartnerManager : MonoBehaviour
{
    [SerializeField] PlayerControler _player;

    private void Start()
    {
        Invoke(nameof(InitPartner), 0.5f);
    }
    void InitPartner()
    {
        var partners = GameControler.Instance.partnerTraveler;
        for (int i = 0; i < partners.Count; i++)
        {
            var partner = Instantiate(partners[i].prefab
                , (Vector2)_player.transform.position + _player.PlayerMovement.VectorDirPlayer() * -i
                , Quaternion.identity);
            partner.transform.SetParent(transform);
            float ofset = i + 1;
            partner.GetComponent<BasePartner>().Init(partners[i], _player, this, ofset);
        }
    }
}

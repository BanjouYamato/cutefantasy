using UnityEngine;

public class PlayerFishing : MonoBehaviour
{   
    public bool IsInWater {  get; private set; }
    Observer observer;
    [SerializeField] WeaponManager weaponManager;
    float fishingTime;
    float timer;
    bool wonFish;
    [SerializeField] ItemScriptable fish;
    PlayerControler controler;
    private void Start()
    {   
        observer = Observer.Instance;
        controler = GetComponent<PlayerControler>();
        observer.AddToList(ObserverCostant.OBSERVER_FISHING
            , FacingWater);
        observer.AddToList(ObserverCostant.OBSERVER_ENDFISHING
            , FinishFishing);
    }
    private void OnDestroy()
    {
        observer.RemoveToList(ObserverCostant.OBSERVER_FISHING
            , FacingWater);
        observer.RemoveToList(ObserverCostant.OBSERVER_ENDFISHING
            , FinishFishing);
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && IsInWater)
        {
            FinishFishing();
        }*/
        if (IsInWater)
        {
            timer += Time.deltaTime;
            if (timer >= fishingTime && !wonFish)
            {
                wonFish = true;
                observer.Notify(ObserverCostant.OBSERVER_HITFISH);
            }
        }
    }
    public void FacingWater()
    {
        IsInWater = true;
        fishingTime = Random.Range(5, 15);
    }
    public void OutOfFishing()
    {   
        controler.PlayerAction.PerformingAction = false;
        IsInWater = false;
        wonFish = false;
        timer = 0;
        observer.Notify(ObserverCostant.OBSERVER_WONFISH);
    }
    public void FinishFishing()
    {
        controler.PlayerStateMachine.ChangeState(new EndFishingState(controler, controler.PlayerStats.dir));
        weaponManager.Action();
    }
}

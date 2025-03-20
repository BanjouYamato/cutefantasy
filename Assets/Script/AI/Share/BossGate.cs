using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossGate : MonoBehaviour
{
    [SerializeField]
    GameObject playerMain, playerDummy, bossMain, bossDummy;

    [SerializeField]
    BoxCollider2D col;

    [SerializeField]
    PlayableDirector timeline;

    [SerializeField]
    AudioClip bossMusic;

    [SerializeField]
    SpriteRenderer obstacleBoss;

    public int bossID;

    private void Start()
    {
        timeline.gameObject.SetActive(false);
        if (GameControler.Instance.IsBossDefeated(bossID))
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.TryGetComponent(out PlayerControler controler))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            playerMain.SetActive(false);
            playerDummy.SetActive(true);
            playerDummy.transform.position = playerMain.transform.position;
            bossDummy.SetActive(true);
            timeline.gameObject.SetActive(true);
            timeline.Play();
        }
    }
    public void OnTimelineFinished()
    {
        playerDummy.SetActive(false);
        playerMain.SetActive(true);
        playerMain.transform.position = playerDummy.transform.position;
        bossDummy?.SetActive(false);
        bossMain?.SetActive(true);
        col.enabled = false;
        ObstacleBossPerform(true);
        SoundManager.Instance.BackGroundMusic(bossMusic);
    }

    public void ObstacleBossPerform(bool val)
    {
        if (val)
        {
            obstacleBoss.gameObject.SetActive(true);
            obstacleBoss.DOFade(1, 0.5f);
        }
        else
        {
            obstacleBoss.DOFade(0, 0.5f).OnComplete(() =>
            {
                obstacleBoss.gameObject.SetActive(false);
            });
        }
    }
}

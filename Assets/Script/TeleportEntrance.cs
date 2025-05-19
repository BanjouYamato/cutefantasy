using UnityEngine;

public class TeleportEntrance : MonoBehaviour
{
    public string telePlaceName;
    public string transName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.TryGetComponent(out PlayerControler player))
        {   if (!GameControler.Instance.IsChangScene)
                GameControler.Instance.IsChangScene = true;
            SceneControler.Instance.MoveToScene(telePlaceName); 
            SceneControler.Instance.SetTransName(transName);
        }
    }
}

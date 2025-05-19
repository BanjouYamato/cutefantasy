using UnityEngine;

public class PlayerArrow : Arrow
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControler>() == null)
        {
            gameObject.SetActive(false);
        }
    }
}

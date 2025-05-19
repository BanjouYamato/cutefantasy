using UnityEngine;

public class LaserBody : MonoBehaviour
{
    public Transform laserStart;

    BoxCollider2D col;
    SpriteRenderer sp;
    float length;
    float speedShoot = 50f;
    bool isHit;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (isHit)
        {
            sp.size = new Vector2(sp.size.x,
            length);
            col.size = new Vector2(col.size.x,
                length);
            length += Time.deltaTime * speedShoot;
        }
    }
    public void StartLaser()
    {
        gameObject.SetActive(true);
        length = 0;
        transform.position = laserStart.position;
        isHit = true;
        
    }
    public void IsHitOff()
    {
        isHit = false;
        gameObject.SetActive(false);
    }
}

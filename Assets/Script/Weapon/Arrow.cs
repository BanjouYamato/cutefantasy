using UnityEngine;

public abstract class Arrow : MonoBehaviour
{
    float speed = 20f;
    private void Update()
    {
        Finish();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * Time.fixedDeltaTime * speed);
    }
    void Finish()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if ( screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1 )
        {
            gameObject.SetActive(false);
        }
    }
    protected abstract void OnTriggerEnter2D(Collider2D collision);
}

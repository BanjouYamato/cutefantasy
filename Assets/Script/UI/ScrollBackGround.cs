using UnityEngine;
using UnityEngine.UI;

public class ScrollBackGround : MonoBehaviour
{
    [SerializeField] RawImage _rawImage;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _magnitude = 0.01f;

    private Vector2 _startPosition;

    private void Start()
    {
        _startPosition = _rawImage.uvRect.position;
    }

    void Update()
    {
        float offsetX = Mathf.Sin(Time.time * _speed) * _magnitude;
        float offsetY = Mathf.Cos(Time.time * _speed) * _magnitude;

        _rawImage.uvRect = new Rect(_startPosition + new Vector2(offsetX, offsetY), _rawImage.uvRect.size);
    }
}

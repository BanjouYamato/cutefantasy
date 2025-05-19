using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] Image _image;

    public void UpdateHealthBar(float health)
    {
        _image.DOFillAmount(health, 0.5f);
    }
}

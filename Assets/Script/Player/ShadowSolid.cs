using UnityEngine;

public class ShadowSolid : MonoBehaviour
{
    SpriteRenderer sprite;
    Shader shader;
    public Color Color;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        shader = Shader.Find("GUI/Text Shader");
    }
    private void Update()
    {
        ColorSprite();
    }
    void ColorSprite()
    {
        sprite.material.shader = shader;
        sprite.color = Color;
    }
    public void Finish()
    {
        gameObject.SetActive(false);
    }
}

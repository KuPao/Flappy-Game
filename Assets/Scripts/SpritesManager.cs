using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesManager : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    private Sprite current;

    [SerializeField]
    private SpriteRenderer s_renderer;

    void OnEnable()
    {
        Flappy.change_sprite += ChangeSprite;

        current = sprites[0];
        s_renderer.sprite = current;
    }

    void OnDisable() {
        Flappy.change_sprite -= ChangeSprite;
    }

    void ChangeSprite()
    {
        if (current == sprites[0]) {
            current = sprites[1];
            s_renderer.sprite = current;
        }
        else {
            current = sprites[0];
            s_renderer.sprite = current;
        }
    }
}

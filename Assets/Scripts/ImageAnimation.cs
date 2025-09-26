using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour {

    public Sprite[] sprites;
    public int spritePerFrame = 6;
    public bool loop = true;
    public bool destroyOnEnd = false;

    private int index = 0;
    private int direction = 1; // 1 for forward, -1 for backward
    private Image image;
    private int frame = 0;

    void Awake() {
        image = GetComponent<Image>();
    }

    void Update ()
    {
        if (!loop && ((direction == 1 && index == sprites.Length) || (direction == -1 && index < 0))) return;
        frame++;
        if (frame < spritePerFrame) return;
        frame = 0;
        image.sprite = sprites[index];
        index += direction;
        if (index >= sprites.Length) {
            if (loop) {
                direction = -1;
                index = sprites.Length - 2; // Step back to avoid overflow
            }
            if (destroyOnEnd) Destroy(gameObject);
        } else if (index < 0) {
            if (loop) {
                direction = 1;
                index = 1; // Step forward to avoid underflow
            }
            if (destroyOnEnd) Destroy(gameObject);
        }
    }
}
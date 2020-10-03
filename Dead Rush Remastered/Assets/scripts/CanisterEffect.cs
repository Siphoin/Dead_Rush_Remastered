using System.Collections;
using UnityEngine;

public class CanisterEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Color alpha_color;


    // Use this for initialization
    void Start()
    {
        var color_a = Color.white;
        color_a.a = 0;
        alpha_color = color_a;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(EffectScale());
        StartCoroutine(EffectAlpha());
    }


    IEnumerator EffectAlpha()
    {
        yield return new WaitForSeconds(3);
        float t = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            t += 0.01f;
            spriteRenderer.color = Color.Lerp(Color.white, alpha_color, t);
            if (spriteRenderer.color.a == 0)
            {
                Destroy(gameObject);
            }
        }

    }

    IEnumerator EffectScale()
    {
        transform.localScale = Vector3.zero;
        float t = 0;
        Vector3 vec_scale_r = Vector3.one * 3;
        while (t < 1)
        {
            yield return new WaitForSeconds(0.01f);
            t += 0.07f;
            transform.localScale = Vector3.Lerp(Vector3.zero, vec_scale_r, t);
            spriteRenderer.color = Color.Lerp(alpha_color, Color.white, t);
        }

    }

}
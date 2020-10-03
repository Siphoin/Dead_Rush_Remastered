using System.Collections;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    Sprite[] variants;

    float t = 0;

    Color alpha_color;

    Color active_color;

    // Use this for initialization
    void Start()
    {
        IniSpriteLink();
        NewAlphaColor();
        var zPos = transform.position;
        zPos.z = -1;
        transform.position = zPos;
        variants = Resources.LoadAll<Sprite>("Bloods");
        spriteRenderer.sprite = variants[Random.Range(0, variants.Length)];
        StartCoroutine(EffectAlpha());
    }

    private void NewAlphaColor()
    {

        var color_a = spriteRenderer.color;
        color_a.a = 0;
        alpha_color = color_a;
        active_color = spriteRenderer.color;
    }

    private void IniSpriteLink()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    IEnumerator EffectAlpha()
    {
        yield return new WaitForSeconds(6);
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            t += 0.01f;
            spriteRenderer.color = Color.Lerp(active_color, alpha_color, t);
            if (spriteRenderer.color.a == 0)
            {
                Destroy(gameObject);
            }
        }

    }

    public void AcidBlood()
    {
        IniSpriteLink();
        spriteRenderer.color = Color.green;
        NewAlphaColor();

    }
}

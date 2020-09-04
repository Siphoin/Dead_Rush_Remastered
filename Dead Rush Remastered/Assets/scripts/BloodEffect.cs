using UnityEngine;
using System.Collections;

public class BloodEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    Sprite[] variants;

    float t = 0;

    Color alpha_color;
    
    // Use this for initialization
    void Start()
    {
        var color_a = Color.white;
        color_a.a = 0;
        alpha_color = color_a;
        var zPos = transform.position;
        zPos.z = -1;
        transform.position = zPos;
        spriteRenderer = GetComponent<SpriteRenderer>();
        variants = Resources.LoadAll<Sprite>("Bloods");
        spriteRenderer.sprite = variants[Random.Range(0, variants.Length)];
        StartCoroutine(EffectAlpha());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EffectAlpha ()
    {
        yield return new WaitForSeconds(6);
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
}

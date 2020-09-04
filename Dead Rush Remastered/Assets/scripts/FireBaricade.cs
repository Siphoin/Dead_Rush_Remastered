using UnityEngine;
using System.Collections;

public class FireBaricade : MonoBehaviour
{
    private Baricade baricade;

    private Color endLiveColor;
    private SpriteRenderer spriteRenderer;
    private float t;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var endColor = Color.black;
        endColor.a = 0;
        endLiveColor = endColor;
        baricade = GameObject.FindGameObjectWithTag("Baricades").GetComponent<Baricade>();
        StartCoroutine(DamageBaricade());
        StartCoroutine(EffectAlpha());
    }

    // Update is called once per frame
    void Update()
    {
        if (baricade == null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DamageBaricade  ()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            baricade.DamageBaricade(1);
        }
    }

    IEnumerator EffectAlpha()
    {
        yield return new WaitForSeconds(7);
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            t += 0.01f;
            spriteRenderer.color = Color.Lerp(Color.white, endLiveColor, t);
            if (spriteRenderer.color.a == 0)
            {
                Destroy(gameObject);
            }
        }

    }


}

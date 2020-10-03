using System;
using System.Collections;
using UnityEngine;

public class GhostZombie : MonoBehaviour, IDisposable
{
    private SpriteRenderer spriteRenderer;
    private ZombieBase zombieBase;
    private Color alpha_color = new Color();

    private float t = 0;

    private const float T_COF = 0.1f;

    private bool back_color_started = false;
    // Use this for initialization
    void Start()
    {
        var a_color = Color.white;
        a_color.a = 0;
        alpha_color = a_color;
        spriteRenderer = GetComponent<SpriteRenderer>();
        zombieBase = GetComponent<ZombieBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (t < 1 && !back_color_started)
        {
            t += T_COF;
            spriteRenderer.color = Color.Lerp(Color.white, alpha_color, t);
        }

        zombieBase.SetStateVisibleUI(spriteRenderer.color.a > 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Baricades":
                ReturnToMaterialWorld();

                break;

            case "Player":
                ReturnToMaterialWorld();

                break;

            case "Partner":
                ReturnToMaterialWorld();

                break;
        }

    }

    private void ReturnToMaterialWorld()
    {
        if (!back_color_started)
        {
            back_color_started = true;
            StartCoroutine(BackWhiteColor());

        }
    }

    public void Dispose()
    {
        spriteRenderer = null;
        alpha_color = new Color();
        zombieBase = null;
        t = 0;
    }

    private IEnumerator BackWhiteColor()
    {
        t = 0;
        while (t < 1)
        {
            yield return new WaitForSeconds(T_COF);
            t += T_COF;
            spriteRenderer.color = Color.Lerp(alpha_color, Color.white, t);


            if (t >= 1)
            {
                yield break;
            }
        }
    }
}
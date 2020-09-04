using UnityEngine;
using System.Collections;
using TMPro;

public class WaveText : MonoBehaviour
{
    float t = 0;
    Color alpha_color;
    Color original_color;
    TextMeshProUGUI textMesh;
    // Use this for initialization
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        transform.localScale *= 10;
        var c = textMesh.color;      
        c.a = 0;
        alpha_color = c;
        original_color = textMesh.color;
        StartCoroutine(Animation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Animation ()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            t += 0.03f;
            transform.localScale = Vector3.Lerp(Vector3.one * 10, Vector3.one, t);
            if (transform.localScale == Vector3.one)
            {
                StartCoroutine(AnimationEnd());
                yield break;
            }
        }
    }

    IEnumerator AnimationEnd()
    {
        t = 0;
        yield return new WaitForSeconds(5f);
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            t += 0.01f;
            textMesh.color = Color.Lerp(original_color, alpha_color, t);
            textMesh.outlineColor = Color.Lerp(Color.black, alpha_color, t);

            if (textMesh.color == alpha_color)
            {
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using UnityEngine;

public class MenuZombie : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    const float speed = 1;
    Vector3 point;
    const float WIDTH_SCREEN = 8.57F;
    const float HEIGHT_SCREEN = 5.18f;
    float t_angle = 0;
    float angleEnd = 0;
    // Use this for initialization
    void Start()
    {
        point = transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Walking());
        float x = Random.Range(WIDTH_SCREEN * -1, WIDTH_SCREEN);
        float y = Random.Range(HEIGHT_SCREEN * -1, HEIGHT_SCREEN);
        transform.position = new Vector3(x, y, -4);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, point) > 0.1f)
        {
            float angle = Mathf.Atan2(transform.position.y - point.y, transform.position.x - point.x) * Mathf.Rad2Deg;
            t_angle += 0.01F;
            float angleLerp = Mathf.Lerp(angleEnd, angle, t_angle);
            transform.rotation = Quaternion.Euler(0, 0, angleLerp);
            if (angleLerp == angle)
            {
                rigidbody2D.MovePosition(Vector2.MoveTowards(transform.position, point, speed * Time.deltaTime));
            }
        }



    }

    IEnumerator Walking()
    {
        while (true)
        {

            yield return new WaitForSeconds(Random.Range(6, 31));
            NewPoint();

        }
    }

    private void NewPoint()
    {
        float x = Random.Range(WIDTH_SCREEN * -1, WIDTH_SCREEN);
        float y = Random.Range(HEIGHT_SCREEN * -1, HEIGHT_SCREEN);
        point = new Vector3(x, y, 0);
        t_angle = 0;
        angleEnd = transform.rotation.z;
    }
}

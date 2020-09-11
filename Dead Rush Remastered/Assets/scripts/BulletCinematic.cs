using UnityEngine;

public class BulletCinematic : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed;

    private DialogPublisher dialogPublisherTarget = null;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.right * speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, Camera.main.transform.position) > 15)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(DialogPublisher publisher)
    {
        dialogPublisherTarget = publisher;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dialogPublisherTarget != null)
        {
            if (collision.gameObject == dialogPublisherTarget.gameObject)
            {
                dialogPublisherTarget.OnDead();
                Destroy(gameObject);
            }
        }
    }
}

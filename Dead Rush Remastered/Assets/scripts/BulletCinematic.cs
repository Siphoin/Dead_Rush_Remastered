using UnityEngine;

public class BulletCinematic : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed;
    BulletCinematicDirection cinematicDirection = BulletCinematicDirection.Left;

    private DialogPublisher dialogPublisherTarget = null;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (cinematicDirection == BulletCinematicDirection.Right)
        {
            spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Vector3.zero;
        switch (cinematicDirection)
        {
            case BulletCinematicDirection.Left:
                dir = transform.right * -1;
                break;

            case BulletCinematicDirection.Right:
                dir = transform.right;
                break;
        }
        transform.Translate(dir * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, Camera.main.transform.position) > 15)
        {
            Destroy(gameObject);
        }
    }

    public void SetTypeDirection(BulletCinematicDirection type_dir)
    {
        cinematicDirection = type_dir;
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

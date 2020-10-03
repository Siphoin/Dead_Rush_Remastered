using UnityEngine;

public class MovedObject : MonoBehaviour
{
    [SerializeField] CinematicDirection direction = CinematicDirection.Right;
    [SerializeField] float speed;
    [SerializeField] bool isStarted = false;
    public bool moving { get; set; } = false;
    // Use this for initialization
    void Start()
    {
        moving = isStarted;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Vector2 dir = Vector3.zero;
            switch (direction)
            {
                case CinematicDirection.Left:
                    dir = transform.right;
                    break;

                case CinematicDirection.Right:
                    dir = transform.right * -1;
                    break;
            }
            transform.Translate(dir * speed * Time.deltaTime);

        }



    }
}

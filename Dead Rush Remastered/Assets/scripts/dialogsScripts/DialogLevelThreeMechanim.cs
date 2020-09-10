using UnityEngine;

public class DialogLevelThreeMechanim : DialogMechanimScript
{
    [SerializeField] SpriteRenderer spriteRenderer_dmitril;
    bool walk_dmitril;
    // Use this for initialization
    void Start()
    {
        actionsContainer = new DialogAction[]
        {
            new DialogAction(6, DmitrilWalk)
        };
        PublishEvent(DmitrilWalk, actionsContainer[0].index);
    }

    private void DmitrilWalk()
    {
        spriteRenderer_dmitril.flipX = false;
        walk_dmitril = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (walk_dmitril)
        {
            spriteRenderer_dmitril.transform.Translate(transform.right * 0.5f * Time.deltaTime);
        }
    }
}

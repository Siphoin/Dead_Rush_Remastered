using UnityEngine;
using System.Collections;
using System;

public class DialogLevelEightMechanim : DialogMechanimScript
{
    [SerializeField] DialogPublisher player;
    [SerializeField] MovedObject zombie;
    private MovedObject player_movedObject;
    // Use this for initialization
    void Start()
    {
        player_movedObject = player.GetComponent<MovedObject>();
        actionsContainer = new DialogAction[]
        {
            new DialogAction(2, ZombieAttack)
        };
        PublishEvent(ZombieAttack, actionsContainer[0].index);
    }

    private void ZombieAttack()
    {
        StartCoroutine(ZombieAttackTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ZombieAttackTime ()
    {
        yield return new WaitForSeconds(7.2f);
        zombie.moving = true;
        yield return new WaitForSeconds(2);
        player_movedObject.moving = false;
        player.OnFire(CinematicDirection.Left, zombie.GetComponent<DialogPublisher>());
        while (zombie != null)
        {
            yield return new WaitForSeconds(1 / 60);
        }
        yield return new WaitForSeconds(6f);
        player_movedObject.moving = true;
    }
}

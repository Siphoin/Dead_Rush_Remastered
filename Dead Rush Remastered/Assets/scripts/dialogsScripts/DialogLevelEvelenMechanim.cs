using System.Collections;
using UnityEngine;

public class DialogLevelEvelenMechanim : DialogMechanimScript
{
    [SerializeField] DialogPublisher player;
    private MovedObject player_movedObject;
    [SerializeField] MovedObject car_player;
    [SerializeField] MovedObject car_dmitril;
    [SerializeField] MovedObject dmitril;
    // Use this for initialization
    void Start()
    {

        player_movedObject = player.GetComponent<MovedObject>();
        actionsContainer = new DialogAction[]
        {
            new DialogAction(3, StartMechanimStandPlayer),
            new DialogAction(7, DmitrilTalk),
            new DialogAction(14, PlayerKillDmitril),
        };

        PublishEvent(StartMechanimStandPlayer, actionsContainer[0].index);
        PublishEvent(DmitrilTalk, actionsContainer[1].index);
        PublishEvent(PlayerKillDmitril, actionsContainer[2].index);
    }

    private void PlayerKillDmitril()
    {
        BulletCinematic bullet = player.OnFire(dmitril.GetComponent<DialogPublisher>());
        bullet.transform.eulerAngles = player.transform.eulerAngles;
        StartCoroutine(PlayerContinueRacing());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartMechanimStandPlayer()
    {
        dmitril.gameObject.SetActive(false);
        StartCoroutine(OnStandPlayer());
    }

    void DmitrilTalk()
    {
        StartCoroutine(DmitrilTalking());
    }

    private IEnumerator OnStandPlayer()
    {
        yield return new WaitForSeconds(1);
        player_movedObject.moving = false;
        yield return new WaitForSeconds(2);
        player_movedObject.moving = true;
        DialogComponent.Active = false;
        yield return new WaitForSeconds(11);
        player_movedObject.moving = false;
        DialogComponent.Active = true;
        yield return new WaitForSeconds(3);
        Camera.main.transform.SetParent(null);
        player.gameObject.SetActive(false);
        player.transform.SetParent(car_player.transform);
        car_player.moving = true;
        car_player.transform.eulerAngles = new Vector3(0, 0, -90);
        Camera.main.transform.SetParent(car_player.transform);
        DialogComponent.Active = false;
        car_dmitril.moving = true;
        yield return new WaitForSeconds(12);
        DialogComponent.Active = true;
        yield return new WaitForSeconds(1f);
        DialogComponent.Active = false;
        car_dmitril.moving = false;
        car_player.moving = false;
        yield return new WaitForSeconds(1);
        DialogComponent.Active = true;
    }
    IEnumerator DmitrilTalking()
    {
        dmitril.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        player.transform.eulerAngles = new Vector3(0, 0, 39);
        dmitril.transform.eulerAngles = new Vector3(0, 0, -150);
        yield return null;
    }

    IEnumerator PlayerContinueRacing()
    {
        yield return new WaitForSeconds(3);
        player.gameObject.SetActive(false);
        player.transform.SetParent(car_player.transform);
        car_player.moving = true;
    }

}

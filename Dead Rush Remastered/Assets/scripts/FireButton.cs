using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireButton : MonoBehaviour
{
    private Character Player;
    Button button;
    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(OnFireTouch));
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnFireTouch()
    {
        Player.OnFire();
    }

}

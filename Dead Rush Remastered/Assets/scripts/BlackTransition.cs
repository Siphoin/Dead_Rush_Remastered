using System;
using UnityEngine;
using UnityEngine.UI;

public class BlackTransition : MonoBehaviour
{
    private BlackTransitionType transitionType = BlackTransitionType.Reverse;
    private Color a_color = new Color32(0, 0, 0, 0);
    private float t = 0;
    const float TIME_ALPHA = 0.008f;
    [SerializeField] private Image fon;

    public event Action action;
    // Use this for initialization
    void Start()
    {
        if (transitionType == BlackTransitionType.Next)
        {
            fon.color = a_color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (t >= 1f)
        {
            action?.Invoke();
            if (action == null)
            {
                Destroy(gameObject);
            }
        }
        t += TIME_ALPHA;
        switch (transitionType)
        {
            case BlackTransitionType.Next:
                fon.color = Color.Lerp(a_color, Color.black, t);
                break;
            case BlackTransitionType.Reverse:
                fon.color = Color.Lerp(Color.black, a_color, t);
                break;
        }

    }

    public void SetType(BlackTransitionType TransitionType)
    {
        transitionType = TransitionType;
    }
}
using UnityEngine;
using System.Collections;

public class AnimatedWindow : MonoBehaviour
{
    float t = 0;
    protected GameObject target_window;

    public bool AnimIsFinish { get => t >= 1; }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void AnimationProcess()
    {
        t += 0.03f;
        LerpingWindow();
    }

    private void LerpingWindow()
    {
        target_window.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
    }

    protected void StartAnimation ()
    {
        LerpingWindow();
    }
}

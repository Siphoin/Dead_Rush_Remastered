using System;

public class Window : AnimatedWindow
{
    public event Action exitEvent;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Exit()
    {
        exitEvent?.Invoke();
        Destroy(gameObject);
    }

    protected void SetChildEvent (Action method)
    {
        exitEvent += method;
    }


}

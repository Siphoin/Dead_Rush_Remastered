using System;

public class Window : AnimatedWindow
{
    public event Action exitEvent;

    public void Exit()
    {
        exitEvent?.Invoke();
        Destroy(gameObject);
    }

    protected void SetChildEvent(Action method)
    {
        exitEvent += method;
    }


}

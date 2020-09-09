using UnityEngine;
public abstract    class BlackTransitionCaller : MonoBehaviour
    {

    public static BlackTransition Create (BlackTransitionType type = BlackTransitionType.Reverse)
    {
        BlackTransition transition = Instantiate(Resources.Load<BlackTransition>("Prefabs/BlackTransition"));
        transition.SetType(type);
        return transition;
    }
    }

using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    [SerializeField] float time;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, time);
    }

}

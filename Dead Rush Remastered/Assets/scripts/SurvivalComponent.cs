using UnityEngine;
using System.Collections;

public class SurvivalComponent : MonoBehaviour
{
  [SerializeField]  private LevelManager levelManager;
    // Use this for initialization
    void Awake()
    {
        if (levelManager == null)
        {
            throw new System.NullReferenceException("level manager is null");
        }

        else
        {
            levelManager.SetCustomParams(int.MaxValue);
            Destroy(this);
        }
    }

}

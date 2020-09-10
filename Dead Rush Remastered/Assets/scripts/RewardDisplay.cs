using UnityEngine;
using UnityEngine.UI;

public class RewardDisplay : MonoBehaviour
{
    [SerializeField] Text text_reward;
    const float speed = 1;
    private float t;
    Color alpha_color;
    // Use this for initialization
    void Start()
    {
        var c = Color.white;
        c.a = 0;
        alpha_color = c;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime);
        t += 0.01f;
        text_reward.color = Color.Lerp(Color.white, alpha_color, t);
        if (text_reward.color.a == 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnDisplayReward(int reward_value)
    {
        text_reward.text = $"+ {reward_value}$";
    }
}

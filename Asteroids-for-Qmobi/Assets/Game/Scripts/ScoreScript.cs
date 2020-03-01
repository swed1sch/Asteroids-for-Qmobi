using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int _score;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + _score;
    }
}

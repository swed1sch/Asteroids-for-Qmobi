
using UnityEngine;
using UnityEngine.UI;
public class Life : MonoBehaviour
{
    public static int lifeValue = 3;
    Text life;

    // Start is called before the first frame update
    
    void Start()
    {
        life = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        life.text = "Life: " + lifeValue;
    }
}

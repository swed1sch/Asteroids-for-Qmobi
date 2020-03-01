using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject rocket;
    private int _startLevelAsteroidNum;
    private bool _allAsteroidsOffScreen;
    private int levelAsteroidNum;
    private Camera mainCam;
    private int asteroidLife;


    // Start is called before the first frame update
    void Start()
    {
        Life.lifeValue = 3;
        ScoreScript._score = 0;
        asteroid.SetActive(false);
        mainCam = Camera.main;
        _startLevelAsteroidNum = 3;
        CreateAsteroids(_startLevelAsteroidNum);
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.8f);

        if (asteroidLife <= 0)
        {
            asteroidLife = 6;
            CreateAsteroids(2);
        }

        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;
        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight/ 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        _allAsteroidsOffScreen = true;
    }

    private void CreateAsteroids(int asteroidsNum)
    {
        for(int i = 1; i <= asteroidsNum; i++)
        {
            GameObject AsteroidClone = Instantiate(asteroid, new Vector2(Random.Range(-10, 10), 6f), transform.rotation);
            AsteroidClone.GetComponent<Asteroid>().SetGeneration(1);
            AsteroidClone.SetActive(true);
        }
    }
    
    public void RocketFail()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(1);
        
    }

    public void asteroidDestroyed()
    {
        asteroidLife--;
    }
    public int startLevelAsteroidsNum
    {
        get { return _startLevelAsteroidNum; }
    }
    public bool allAsteroidsOffScreen
    {
        get { return _allAsteroidsOffScreen; }
    }
}

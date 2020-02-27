using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        asteroid.SetActive(false);
        mainCam = Camera.main;
        _startLevelAsteroidNum = 2;
        CreateAsteroids(_startLevelAsteroidNum);
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.deltaTime * 0.8f);

        if (asteroidLife <= 0)
        {
            asteroidLife = 6;
            CreateAsteroids(1);
        }

        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;
        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneWidth * -1;
        float sceneTopEdge = sceneWidth / 2;
        float sceneBottomEdge = sceneWidth * -1;
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
        print("GAME OVER");
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

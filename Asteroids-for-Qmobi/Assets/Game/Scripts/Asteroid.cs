
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject rock;
    public GamePlay gamePlay;
    private float maxRotation;
    private float rotationX;
    private float rotationY;
    private float rotationZ;
    private Rigidbody rg;
    private Camera mainCam;
    private float maxSpeed;
    private int _generation;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        maxRotation = 25f;
        rotationX = Random.Range(-maxRotation, maxRotation);
        rotationY = Random.Range(-maxRotation, maxRotation);
        rotationZ = Random.Range(-maxRotation, maxRotation);

        rg = rock.GetComponent<Rigidbody>();

        float speedX = Random.Range(200f, 800f);
        int selectorX = Random.Range(0, 2);
        float dirX = 0;
        if (selectorX == 1) { dirX = -1; }
        else { dirX = 1; }
        float finalSpeedX = speedX * dirX;
        rg.AddForce(transform.right*finalSpeedX);

        float speedY = Random.Range(200f, 800f);
        int selectorY = Random.Range(0, 2);
        float dirY = 0;
        if (selectorY == 1) { dirY = -1; }
        else { dirY = 1; }
        float finalSpeedY = speedY * dirY;
        rg.AddForce(transform.up * finalSpeedY );
    }

    public void SetGeneration(int generation)
    {
        _generation = generation;
    }
    // Update is called once per frame
    void Update()
    {
        rock.transform.Rotate(new Vector3(rotationX, rotationY, 0) * Time.deltaTime);
        CheckPosition();
        float dynamicMaxSpeed = 3f;
        rg.velocity = new Vector2(Mathf.Clamp(rg.velocity.x, -dynamicMaxSpeed, dynamicMaxSpeed), Mathf.Clamp(rg.velocity.y, -dynamicMaxSpeed, dynamicMaxSpeed));
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Bullet(Clone)")
        {
            if (_generation < 3)
            {
                CreateSmallAsteroids(2);
            }               
            Destroy();
        }
        if (collision.collider.name == "Rocket")
        {
            gamePlay.RocketFail();
        }          
    }

    void CreateSmallAsteroids(int asteroidsNum)
    {
        int newGeneration = _generation + 1;
        for(int i = 1; i < asteroidsNum; i++)
        {
            float scaleSize = 0.5f;
            GameObject AsteroidClone = Instantiate(rock, new Vector3(transform.position.x, transform.position.y, 0f), transform.rotation);
            AsteroidClone.transform.localScale =
                new Vector3(AsteroidClone.transform.localScale.x * scaleSize, AsteroidClone.transform.localScale.y * scaleSize, AsteroidClone.transform.localScale.z * scaleSize );
            AsteroidClone.GetComponent<Asteroid>().SetGeneration(newGeneration);
            AsteroidClone.SetActive(true);  
        }
    }
    
    void CheckPosition()
    {
        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;
        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        float rockOffset;
        if (gamePlay.allAsteroidsOffScreen)
        {
            rockOffset = 1.0f;
            float reverseSpeed = 2000.1f;

            if (rock.transform.position.x > sceneRightEdge + rockOffset)
            {
                rock.transform.rotation = Quaternion.identity;
                rg.AddForce(transform.right * (reverseSpeed * (-1))); 
            }
            if (rock.transform.position.x < sceneLeftEdge - rockOffset)
            {
                rock.transform.rotation = Quaternion.identity;
                rg.AddForce(transform.right * reverseSpeed);
            }
            if (rock.transform.position.y > sceneTopEdge + rockOffset)
            {
                rock.transform.rotation = Quaternion.identity;
                rg.AddForce(transform.up * (reverseSpeed * (-1)));
            }
            if (rock.transform.position.y < sceneBottomEdge - rockOffset)
            {
                rock.transform.rotation = Quaternion.identity;
                rg.AddForce(transform.up * reverseSpeed);
            }
        }
        else
        {
            rockOffset = 2.0f;
            if (rock.transform.position.x > sceneRightEdge + rockOffset)
            {
                rock.transform.position = new Vector2(sceneLeftEdge - rockOffset, rock.transform.position.y);
            }
            if (rock.transform.position.x < sceneLeftEdge - rockOffset)
            {
                rock.transform.position = new Vector2(sceneRightEdge + rockOffset, rock.transform.position.y);
            }
            if (rock.transform.position.y > sceneTopEdge + rockOffset)
            {
                rock.transform.position = new Vector2( rock.transform.position.x,sceneBottomEdge-rockOffset);
            }
            if (rock.transform.position.y < sceneBottomEdge - rockOffset)
            {
                rock.transform.position = new Vector2(rock.transform.position.x, sceneTopEdge + rockOffset);
            }
        }        
    }
    public void Destroy()
    {
        gamePlay.asteroidDestroyed();
        Destroy(gameObject, 0.01f);
    }
    public void DestroySilent()
    {
        Destroy(gameObject, 0.00f);
    }
}

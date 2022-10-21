using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameStarted = false;
    public bool failed = false;

    System.Random rand = new System.Random();

    public List<GameObject> playerPrefabs = new List<GameObject>();

    public List<GameObject> randomObjects = new List<GameObject>();

    public List<GameObject> instantiatedRandomObjects = new List<GameObject>();

    public GameObject backgroundPrefab;

    public List<GameObject> instantiatedBackgrounds = new List<GameObject>();

    float randomObjectYPos;
    float backgroundYPos;

    Vector3 startingPoint = new Vector3(0f, -5.28f, 0f);

    float randomInstantiatingYPos;

    public List<GameObject> soundsPrefabs = new List<GameObject>();
    public AudioSource soundtrack;

    public int selectedBall;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        PlayerPrefs.SetInt("GameCount", 0);

        selectedBall = PlayerPrefs.GetInt("SelectedBall");
        
    }

    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    public void StartingGame()
    {
        
        GameObject player;

        randomObjectYPos = -2f;
        backgroundYPos = 1f;
        randomInstantiatingYPos = 8f;
        CameraController.instance.speed = 0.2f;
        CameraController.instance.distanceToSpeedUp = 14f;
        CameraController.instance.deadYPosition = -10f;
        UIManager.instance.distance = 0f;

        for (int i = 0; i < 2; i++)
        {
            InstantiateBackgrounds();
        }

        player = Instantiate(playerPrefabs[selectedBall], startingPoint, Quaternion.identity);

        soundtrack.Play();
    }

    // Update is called once per frame
    void Update()
    {
        selectedBall = PlayerPrefs.GetInt("SelectedBall");
        if (gameStarted && !failed)
        {
            DestroyOldBackgrounds();
            if (PlayerController.instance.transform.position.y >= randomInstantiatingYPos)
            {
                InstantiateBackgrounds();
                randomInstantiatingYPos += 14f;
            }
            Distance();
        }
    }

    void InstantiateBackgrounds()
    {
        GameObject background;

        background = Instantiate(backgroundPrefab);

        background.transform.position = new Vector3(0f, backgroundYPos, 1f);

        backgroundYPos += 14f;

        instantiatedBackgrounds.Add(background);

        InstantiateRandomObjects();
    }

    void InstantiateRandomObjects()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject randomObject;

            randomObject = Instantiate(randomObjects[rand.Next(0, 4)]);
            float randX = rand.Next(-3, 4);
            randomObject.transform.position = new Vector3(randX, randomObjectYPos, 0f);

            instantiatedRandomObjects.Add(randomObject);
            randomObjectYPos += 2f;
        }
    }

    void DestroyOldBackgrounds()
    {
        if (instantiatedBackgrounds.Count >=6)
        {
            Destroy(instantiatedBackgrounds[0], 0f);
            instantiatedBackgrounds.Remove(instantiatedBackgrounds[0]);
        }
    }

    public void Distance()
    {
        var distance = Vector3.Distance(startingPoint, PlayerController.instance.transform.position);

        if (UIManager.instance.distance<distance)
        {
            UIManager.instance.distance = distance;
        }
    }

    public void InstantiateSound(int index, Vector3 position)
    {
        GameObject sound;

        sound = Instantiate(soundsPrefabs[index], position, Quaternion.identity);

        Destroy(sound, 4f);
    }
}

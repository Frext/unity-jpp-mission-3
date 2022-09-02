using UnityEngine;

public class ObstacleSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    [Header("Instantiating Obstacles")]
    [SerializeField] private float startDelay = 2;
    [SerializeField] private float repeatRate = 2;

    private Vector3 spawnPos = new Vector3(25, 0, 0);

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    private void SpawnObstacle()
    {
        if (!playerControllerScript.IsGameOver) {

            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity, transform);
        }
        else {
            CancelInvoke(nameof(SpawnObstacle));
        }
    }
}

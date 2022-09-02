using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 30f;

    [Header("Bounds")]
    [SerializeField] private bool shouldDestroyOnExitBounds = false;
    [SerializeField] private float leftBound = -15f; 

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerControllerScript.IsGameOver) {

            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (transform.position.x < leftBound && shouldDestroyOnExitBounds) {

            Destroy(gameObject);
        }
    }
}

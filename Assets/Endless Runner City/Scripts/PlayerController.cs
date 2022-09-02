
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravityModifier = 1;
    [Space]

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private ParticleSystem dirtSplatterParticleSystem;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;

    [HideInInspector] public bool IsGameOver { get; set; }

    AudioSource playerAudio;
    Rigidbody playerRb;
    Animator playerAnim;
    

    private bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        IsGameOver = false;

        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !IsGameOver) {

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            playerAnim.SetTrigger("Jump_trig");
            
            playerAudio.PlayOneShot(jumpSound, 1.0f);

            dirtSplatterParticleSystem.Stop();

            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {

            isOnGround = true;

            dirtSplatterParticleSystem.Play();
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {

            IsGameOver = true;

            explosionParticleSystem.Play();
            dirtSplatterParticleSystem.Stop();

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}

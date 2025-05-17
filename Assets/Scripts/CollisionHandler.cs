using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    Collider collider;

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crush;
    [SerializeField] AudioClip successAudio;

    [SerializeField] ParticleSystem crushParticle;
    [SerializeField] ParticleSystem successParticle;


    bool isTransitioning = false;
    bool collisionDisabled = false;


    const string friendlyTag = "Friendly";
    const string finishTag = "Finish";

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        collider = GetComponent<Collider>();

    }

    void Update()
    {
        SkipLvl(); 
        OffCollider();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case friendlyTag:
                break;
            case finishTag:
                StartSuccessSequence();

                break;
            default:
                StartCrashSequence();

                break;
        }


    }

    void StartCrashSequence()

    {
        isTransitioning = true;
        // audioSource.Stop();
        audioSource.PlayOneShot(crush);
        crushParticle.Play();
        Debug.Log(crushParticle.isPlaying);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLvl", levelLoadDelay);

    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        // audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLvl", levelLoadDelay);

    }

    void ReloadLvl()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;

    }
    void NextLvl()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        isTransitioning = false;

    }

    void SkipLvl()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLvl();

        }

    }

    void OffCollider()
    {
        if (Input.GetKey(KeyCode.C))
        {
        
            collisionDisabled = !collisionDisabled;

        }


    }


}

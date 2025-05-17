using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    AudioSource audioSource;



    [SerializeField] float mainThrust = 1000;
    [SerializeField] float forwardRotate = 1;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem jetParticle;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();


    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
     
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();

        }

    }



    private void StopThrusting()
    {
        audioSource.Stop();
        jetParticle.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!jetParticle.isPlaying)
        {
            jetParticle.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(forwardRotate);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-forwardRotate);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(rotationThisFrame * Vector3.forward * forwardRotate * Time.deltaTime);
        rb.freezeRotation = false;

    }

   

}

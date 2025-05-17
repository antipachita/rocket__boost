using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0, 1)] float movementFactor;
    void Start()
    {
        startingPosition = transform.position;

    }

    void Update()
    {
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
    }
}

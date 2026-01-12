using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    public int rocketSpeed = 1;

    private void Update()
    {
        transform.position += transform.forward * rocketSpeed * Time.deltaTime;
    }
}

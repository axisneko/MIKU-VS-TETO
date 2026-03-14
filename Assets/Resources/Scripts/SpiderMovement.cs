using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public int movementSpeed;

    public GameObject leg;
    void Update()
    {
        transform.position -= new Vector3(0, 0, movementSpeed*Time.deltaTime);
        moveLegs();
    }

    private void moveLegs() 
    {
        if (!leg.GetComponent<SpiderLegMovement>().isTryingToStep)
        {
            leg.transform.position += new Vector3(0, 0, movementSpeed * Time.deltaTime);
        }
    }
}

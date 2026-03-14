using UnityEngine;

public class SpiderLegMovement : MonoBehaviour
{
    public Vector3 legPosition;
    public float legSpeed;
    public float legDistance;

    public bool isTryingToStep = false;

    private void Update()
    {
        if (!isTryingToStep)
        {
            if (legPosition.z-transform.localPosition.z <= -1*legDistance)
            {
                isTryingToStep = true;
            }
        }else
        {
            float yPos = 0;
            if (Mathf.Abs(transform.localPosition.z - legPosition.z) < legDistance)
            {
                yPos = Mathf.Sqrt(Mathf.Pow(legDistance, 2) - Mathf.Pow(transform.localPosition.z - legPosition.z, 2));
            }
            Debug.Log(Vector3.Distance(transform.localPosition, legPosition));

            transform.localPosition += new Vector3(0, yPos, -1*legSpeed*Time.deltaTime);
            if (transform.localPosition.z - legPosition.z >= legDistance)
            {
                isTryingToStep = false;
            }
        }
    }
}

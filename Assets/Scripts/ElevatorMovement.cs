using System.Collections;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    [SerializeField] Vector3 StartingPosition = new Vector3(0, 0, 0);
    [SerializeField] Vector3 ControlPoint = new Vector3(0, 0, 0); // Adjust as needed
    [SerializeField] Vector3 FinalPosition = new Vector3(0, 0, 0);
    [SerializeField] Vector3[] Path;



    [SerializeField] float MoveSpeed = 2f;


    private void Start()
    {
        transform.localPosition = StartingPosition;
    }


    IEnumerator MoveElevator(Vector3[] path, float speed)
    {
        int index = 0;
        var wait = new WaitForFixedUpdate();

        while (index < path.Length)
        {
            Vector3 point = path[index];
            while (Vector3.Distance(transform.localPosition, point) > 0.01f)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, point, speed * Time.fixedDeltaTime);
                yield return wait;
            }
            index++;
        }
    }


    public void OnEnteredArea(GameObject other)
    {
        if (other.CompareTag("player"))
        {
            // Start moving the elevator to the final position
            StartCoroutine(MoveElevator(Path, MoveSpeed));
            other.transform.SetParent(transform);
        }
    }

    public void OnExitedArea(GameObject other)
    {
        if (other.CompareTag("player"))
        {
            other.transform.SetParent(null);
        }
    }


}

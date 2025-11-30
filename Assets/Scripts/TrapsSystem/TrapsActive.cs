using System.Collections;
using UnityEngine;

public class TrapsActive : MonoBehaviour
{
    [SerializeField] Vector3 StartingPosition = new Vector3(0, 0, 0);
    [SerializeField] Vector3 EndingPosition = new Vector3(0, 0, 1);
    [SerializeField] float HitSpeed = 2f;
    [SerializeField] float RetreatSpeed = 1f;

    Coroutine currentCoroutine;

    public void OnEnteredArea(GameObject other)
    {
        if (other)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            // Start moving the trap towards the ending position
            currentCoroutine = StartCoroutine(MoveTrap(EndingPosition, HitSpeed));
        }
    }

    public void OnExitedArea(GameObject other)
    {
        if (other)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(MoveTrap(StartingPosition, RetreatSpeed));
        }
    }

    private IEnumerator MoveTrap(Vector3 targetPosition, float moveSpeed)
    {
        // Move towards the ending position
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }
        
    }

    private void Start()
    {
        transform.localPosition = StartingPosition;
    }
}

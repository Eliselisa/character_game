using System.Collections;
using UnityEngine;

public class TrapsPassive : MonoBehaviour
{
    [SerializeField] Vector3 StartingPosition = new Vector3(0, 0, 0);
    [SerializeField] Vector3 EndingPosition = new Vector3(0, 0, 1);
    [SerializeField] float HitSpeed = 2f;
    [SerializeField] float RetreatSpeed = 1f;
    [SerializeField] float HitInterval = 1f;

    private IEnumerator HitCoroutine ()
    {
        yield return new WaitForSeconds(HitInterval);

        bool movingForward = true;

        while (true)
        {
            
            if (movingForward)
            {
               transform.localPosition = Vector3.MoveTowards(transform.localPosition, EndingPosition, Time.deltaTime * HitSpeed);
                yield return null;

                if (Vector3.Distance(transform.localPosition, EndingPosition) < 0.01f)
                {
                    movingForward = false;
                }
            }

            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, StartingPosition, Time.deltaTime * RetreatSpeed);
                yield return null;
                if (Vector3.Distance(transform.localPosition, StartingPosition) < 0.01f)
                {
                    movingForward = true;
                    yield return new WaitForSeconds(HitInterval);
                }
            }

            

        }
    }

    private void Start()
    {
       transform.localPosition = StartingPosition;
        StartCoroutine(HitCoroutine());
    }
}


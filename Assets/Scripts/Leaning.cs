using System.Collections;
using UnityEngine;

public class Leaning : MonoBehaviour
{
    public Transform PeakLeft;
    public Transform PeakRight;
    public Transform Idle;
    public float lerpTime;
    bool leaning;
    float lerpness;

    float timeElapsed;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!leaning)
            {
                StopAllCoroutines();
                lerpness = 0;
                timeElapsed = 0;

                StartCoroutine(Lerp());
            }

            leaning = true;

            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, PeakLeft.position, lerpness),
                Quaternion.Lerp(transform.rotation, PeakLeft.rotation, lerpness));
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            lerpness = 0;
            StartCoroutine(Lerp());

            leaning = false;
            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, Idle.position, lerpness),
                Quaternion.Lerp(transform.rotation, Idle.rotation, lerpness));
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (!leaning)
            {
                StopAllCoroutines();
                lerpness = 0;
                timeElapsed = 0;

                StartCoroutine(Lerp());
            }

            leaning = true;

            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, PeakRight.position, lerpness),
                Quaternion.Lerp(transform.rotation, PeakRight.rotation, lerpness));
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            lerpness = 0;
            StartCoroutine(Lerp());

            leaning = false;
            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, Idle.position, lerpness),
                Quaternion.Lerp(transform.rotation, Idle.rotation, lerpness));
        }
    } // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator Lerp()
    {
        while (timeElapsed <= lerpTime)
        {
            timeElapsed += Time.deltaTime;
            lerpness = Mathf.Lerp(lerpness, 1, timeElapsed / lerpTime);
            Debug.Log("time:" + timeElapsed);
            Debug.Log("lerpy" + lerpness);
            yield return null;
        }

        lerpness = 1;
    }
}
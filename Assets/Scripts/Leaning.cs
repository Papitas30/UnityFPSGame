using System.Collections;
using UnityEngine;

public class Leaning : MonoBehaviour
{
    public Transform PeakLeft;
    public Transform PeakRight;
    public Transform Idle;
    private float lerpness;
    public float lerpTime;
    private bool leaning;
    float timeElapsed;
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!leaning)
            {
                StopAllCoroutines();
                timeElapsed = 0;
                StartCoroutine(Lerp());
            }
            leaning = true;

            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, PeakLeft.position, lerpness), Quaternion.Lerp(transform.rotation, PeakLeft.rotation, lerpness));
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (!leaning)
            {
                StopAllCoroutines();
                timeElapsed = 0;
                StartCoroutine(Lerp());
            }
            leaning = true;

            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, PeakRight.position, lerpness), Quaternion.Lerp(transform.rotation, PeakRight.rotation, lerpness));
        }
        else
        {
            StopAllCoroutines();
            timeElapsed = 0;
            StartCoroutine(NoLerp());

            leaning = false;
            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, Idle.position, lerpness), Quaternion.Lerp(transform.rotation, Idle.rotation, lerpness));
        }
    }

    IEnumerator NoLerp()
    {

        while (timeElapsed <= lerpTime)
        {
            timeElapsed += Time.deltaTime;
            lerpness = Mathf.Lerp(lerpness, 0, timeElapsed / lerpTime * 1.5f);
            Debug.Log("time:" + timeElapsed);
            Debug.Log("lerpy" + lerpness);
            yield return null;
        }

        lerpness = 0;
    }
    IEnumerator Lerp()
    {

        while (timeElapsed <= lerpTime)
        {
            timeElapsed += Time.deltaTime;
            lerpness = Mathf.Lerp(0.1f, 0.9f, timeElapsed / lerpTime);
            Debug.Log("time:" + timeElapsed);
            Debug.Log("lerpy" + lerpness);
            yield return null;
        }

        lerpness = 0.9f;

    }
}
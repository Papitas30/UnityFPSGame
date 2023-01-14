using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject rotateIcon;
    Vector3 localAngles;
    public float rotatingSpeed;

    AsyncOperation loadingOperation;
    public Slider progressBar;
    // Start is called before the first frame update

    private void Start()
    {
        loadingOperation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadSceneMode.Additive);
        Debug.Log($"Starting loading operation for '{LoadingData.sceneToLoad}'");

    }
    // Update is called once per frame
    void Update()
    {
        localAngles += rotatingSpeed * Time.deltaTime * new Vector3(0, 0, 1);
        rotateIcon.transform.eulerAngles = -localAngles;
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        if (loadingOperation.isDone)
        {
            SceneManager.UnloadSceneAsync("Loading");
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            Debug.Log($"Finished loading operation for '{LoadingData.sceneToLoad}'");
        }
    }
}
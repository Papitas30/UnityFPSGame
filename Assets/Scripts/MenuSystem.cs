using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume postProcessVolume;
    private AmbientOcclusion ambientOcc;
    private MotionBlur motionBlur;
    private Bloom bloom;
    private DepthOfField dof;

    public static bool GamePaused = false;

    public GameObject PauseMenu;
    public GameObject SettingsMenu;

    private void Start()
    {
        Application.targetFrameRate = 60;
        postProcessVolume.profile.TryGetSettings(out ambientOcc);
        postProcessVolume.profile.TryGetSettings(out motionBlur);
        postProcessVolume.profile.TryGetSettings(out bloom);
        postProcessVolume.profile.TryGetSettings(out dof);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
                
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dof.active = true;
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dof.active = false;
    }
    
    public void LoadSettings()
    {
      
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void LoadMenu()

    {
        Resume();
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    public void AmbientOccBool(bool value)
    {
        ambientOcc.active = value;
    }
    public void BloomBool(bool value)
    {
        bloom.active = value;
    }
    public void MotionBlurBool(bool value)
    {
        motionBlur.active = value;
    }
    public void DOFBool(bool value)
    {
        dof.active = value;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    // This function will be called by the PlayerInput component
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) // only trigger once per key press
        {
            if (isPaused)
            {
                Debug.Log("Pause input received"); 
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Ensure time scale is reset
        SceneManager.LoadScene("Title");
        isPaused = false;

    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}

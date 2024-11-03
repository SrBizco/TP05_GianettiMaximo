using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEditor.SearchService;

public class UIManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject pauseMenuPanel; // Panel del men� de pausa

    private bool isGamePaused = false; // Estado del juego

    private void Awake()
    {
        // Aseg�rate de que el objeto no se destruya al cargar una nueva escena
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Asegurarse de que las pantallas de opciones y cr�ditos est�n desactivadas al inicio
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        pauseMenuPanel.SetActive(false); // Aseg�rate de que el men� de pausa est� desactivado
    }
    private void Update()
    {
        // Escuchar la tecla Escape en el UIManager para activar el men� de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Funci�n para cargar la escena de juego
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
        mainPanel.SetActive(false);// Reemplaza "Gameplay" con el nombre de tu escena de juego
    }

    // Funci�n para abrir el panel de opciones
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        mainPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

    // Funci�n para cerrar el panel de opciones
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Gameplay"))
        {
            mainPanel.SetActive(false);
            pauseMenuPanel.SetActive(true);
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            mainPanel.SetActive(true);
            pauseMenuPanel.SetActive(false);
        }
    }

    // Funci�n para abrir el panel de cr�ditos
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        mainPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

    // Funci�n para cerrar el panel de cr�ditos
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // Funci�n para salir del juego
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("El juego se ha cerrado."); // Esto solo se ver� en el Editor de Unity
    }

    // Funci�n para ajustar el volumen de la m�sica
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Convierte el valor de 0-1 a decibeles
    }

    // Funci�n para ajustar el volumen de los efectos de sonido
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); // Convierte el valor de 0-1 a decibeles
    }

    // Funci�n para pausar el juego
    public void TogglePause()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Funci�n para pausar el juego
    private void PauseGame()
    {
        Time.timeScale = 0; // Detiene el tiempo del juego
        pauseMenuPanel.SetActive(true); // Muestra el men� de pausa
        isGamePaused = true; // Cambia el estado a pausado
    }

    // Funci�n para reanudar el juego
    public void ResumeGame()
    {
        Time.timeScale = 1; // Restaura el tiempo del juego
        pauseMenuPanel.SetActive(false); // Oculta el men� de pausa
        isGamePaused = false; // Cambia el estado a no pausado
    }

    // Funci�n para volver al men� principal
    public void BackToMainMenu()
    {
        ResumeGame(); // Aseg�rate de reanudar el juego antes de cargar la escena
        SceneManager.LoadScene("MainMenu"); // Cambia "MainMenu" al nombre correcto de tu escena
    }
}
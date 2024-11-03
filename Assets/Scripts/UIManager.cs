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
    [SerializeField] private GameObject pauseMenuPanel; // Panel del menú de pausa

    private bool isGamePaused = false; // Estado del juego

    private void Awake()
    {
        // Asegúrate de que el objeto no se destruya al cargar una nueva escena
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Asegurarse de que las pantallas de opciones y créditos están desactivadas al inicio
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        pauseMenuPanel.SetActive(false); // Asegúrate de que el menú de pausa esté desactivado
    }
    private void Update()
    {
        // Escuchar la tecla Escape en el UIManager para activar el menú de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Función para cargar la escena de juego
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
        mainPanel.SetActive(false);// Reemplaza "Gameplay" con el nombre de tu escena de juego
    }

    // Función para abrir el panel de opciones
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        mainPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

    // Función para cerrar el panel de opciones
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

    // Función para abrir el panel de créditos
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        mainPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

    // Función para cerrar el panel de créditos
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // Función para salir del juego
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("El juego se ha cerrado."); // Esto solo se verá en el Editor de Unity
    }

    // Función para ajustar el volumen de la música
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Convierte el valor de 0-1 a decibeles
    }

    // Función para ajustar el volumen de los efectos de sonido
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); // Convierte el valor de 0-1 a decibeles
    }

    // Función para pausar el juego
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

    // Función para pausar el juego
    private void PauseGame()
    {
        Time.timeScale = 0; // Detiene el tiempo del juego
        pauseMenuPanel.SetActive(true); // Muestra el menú de pausa
        isGamePaused = true; // Cambia el estado a pausado
    }

    // Función para reanudar el juego
    public void ResumeGame()
    {
        Time.timeScale = 1; // Restaura el tiempo del juego
        pauseMenuPanel.SetActive(false); // Oculta el menú de pausa
        isGamePaused = false; // Cambia el estado a no pausado
    }

    // Función para volver al menú principal
    public void BackToMainMenu()
    {
        ResumeGame(); // Asegúrate de reanudar el juego antes de cargar la escena
        SceneManager.LoadScene("MainMenu"); // Cambia "MainMenu" al nombre correcto de tu escena
    }
}
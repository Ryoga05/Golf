using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private Slider slider;
    public static GameManager instance;
    public string playerInputText;
    public int tocs = 0;

    private void Awake()
    {
        // Si no existe ninguna instancia, asignamos esta como la instancia única
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No se destruye al cargar una nueva escena
        }
        else
        {
            // Si ya existe una instancia, destruimos esta para mantener el Singleton
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        // Suscribir al evento sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desuscribir del evento sceneLoaded para evitar problemas de memoria
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Método que se llama automáticamente cuando se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject sliderObject = GameObject.FindGameObjectWithTag("Slider");
        slider = sliderObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetComponent<Player>().fuerza;
    }
}

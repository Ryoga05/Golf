using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
        
        if (sliderObject != null)
        {
            slider = sliderObject.GetComponent<Slider>();
        }

        if (scene.name == "EndScreen")
        {
            // Busca el objeto de texto y actualiza su valor
            TextMeshProUGUI textoNombre = GameObject.Find("Nom")?.GetComponent<TextMeshProUGUI>();
            if (textoNombre != null)
            {
                textoNombre.text = playerInputText;
            }
            TextMeshProUGUI textoTocs = GameObject.Find("Tocs")?.GetComponent<TextMeshProUGUI>();
            if (textoTocs != null)
            {
                textoTocs.text = "Tocs: " + tocs.ToString();
            }
            GuardarNuevoRegistro(playerInputText, tocs);
        }
    }

    void GuardarNuevoRegistro(string nombre, int tocs)
{
    List<(string nombre, int tocs)> ranking = new List<(string, int)>();

    // Cargar registros existentes
    for (int i = 0; i < 5; i++)
    {
        string nomKey = "Rank" + i + "_Nombre";
        string tocKey = "Rank" + i + "_Tocs";

        if (PlayerPrefs.HasKey(nomKey) && PlayerPrefs.HasKey(tocKey))
        {
            ranking.Add((PlayerPrefs.GetString(nomKey), PlayerPrefs.GetInt(tocKey)));
        }
    }

    // Agregar el nuevo registro
    ranking.Add((nombre, tocs));

    // Ordenar por menos tocs
    ranking.Sort((a, b) => a.tocs.CompareTo(b.tocs));

    // Mantener solo los 5 mejores
    while (ranking.Count > 5)
        ranking.RemoveAt(ranking.Count - 1);

    // Guardar de nuevo
    for (int i = 0; i < ranking.Count; i++)
    {
        PlayerPrefs.SetString("Rank" + i + "_Nombre", ranking[i].nombre);
        PlayerPrefs.SetInt("Rank" + i + "_Tocs", ranking[i].tocs);
    }

    PlayerPrefs.Save();
}

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
        {
            slider.value = player.GetComponent<Player>().fuerza;
        }
    }
}

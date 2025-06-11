using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField inputField;  // Asigna el Input Field de TextMeshPro en el Inspector

    private void Start()
    {
        // Suscribir el método OnInputChanged al evento de cambio de texto
        inputField.onValueChanged.AddListener(OnInputChanged);
    }

    private void OnInputChanged(string text)
    {
        // Almacena el texto en el GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.playerInputText = text;
        }
    }
}

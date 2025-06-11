using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingManager : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            string nomKey = "Rank" + i + "_Nombre";
            string tocKey = "Rank" + i + "_Tocs";

            GameObject entry = GameObject.Find((i + 1).ToString()); // GameObject "1", "2", ..., "5"

            if (entry != null)
            {
                TextMeshProUGUI nomText = entry.transform.Find("Nom")?.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI copsText = entry.transform.Find("Cops")?.GetComponent<TextMeshProUGUI>();

                if (PlayerPrefs.HasKey(nomKey) && PlayerPrefs.HasKey(tocKey))
                {
                    if (nomText != null) nomText.text = PlayerPrefs.GetString(nomKey);
                    if (copsText != null) copsText.text = PlayerPrefs.GetInt(tocKey).ToString();
                }
                else
                {
                    if (nomText != null) nomText.text = "-";
                    if (copsText != null) copsText.text = "-";
                }
            }
        }
    }
}

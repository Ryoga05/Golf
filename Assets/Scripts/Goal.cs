using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private GameObject player;
    public string nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            player = other.gameObject;
            Debug.Log("Has guanyat!!!");
            Invoke("NextLevel", 1);
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}

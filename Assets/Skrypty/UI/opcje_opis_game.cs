using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class opcje_opis_game : MonoBehaviour
{
    public static opcje_opis_game instance;
    private Transform t�o_transform;
    private Transform przycisk_transform;
    private Transform wyjscie_transform;
    private Transform menu_transform;
    private void Awake()
    {
        instance = this;
        t�o_transform = transform.Find("T�o");
        wyjscie_transform = transform.Find("wyjscie");
        menu_transform = transform.Find("menu");
        //przycisk_transform = transform.Find("")
        gameObject.SetActive(false);
    }
    
    private void Start()
    {
        //transform.Find("T�o").gameObject.SetActive(false);
        
        PauseGame();
        
        wyjscie_transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            ResumeGame();
            ButtonHandler_game xd = wyjscie_transform.GetComponent<ButtonHandler_game>();
            UIController.instance.ReaktuywujUI();
            gameObject.SetActive(false);
            
        });
        menu_transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            ResumeGame();
            SceneManager.LoadScene("MenuG��wne");
        });
        
    }
    private void Update()
    {
        if(Time.timeScale > 0)
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
            UIController.instance.ReaktuywujUI();
            gameObject.SetActive(false);

        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0f; // Ustaw czas gry na 0, co zatrzyma wi�kszo�� aktywno�ci w grze
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Wznowienie czasu gry
    }
}

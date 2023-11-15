using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlowManager : MonoBehaviour
{
    public event EventHandler Rozwin_opcje;
    [SerializeField]private string NameScene;

    private Transform Nowagra_Transform;
    private Transform Opcje_Transform;
    private Transform Wyjscie_Transform;
    public static FlowManager Instance { get; private set; }
    private void Awake()
    {
        Nowagra_Transform = transform.Find("Nowa_gra_btn");
        Opcje_Transform = transform.Find("Opcje_btn");
        Wyjscie_Transform = transform.Find("Wyjscie_btn");
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Nowagra_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(NameScene);
        });
        Opcje_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Opcje_Transform.GetComponent<ButtonHandler>().Reset();
            gameObject.SetActive(false);
            Rozwin_opcje?.Invoke(this, EventArgs.Empty);
        });
        Wyjscie_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    
}

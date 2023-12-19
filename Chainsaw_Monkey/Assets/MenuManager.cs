using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject comoJogar;

    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    public void ComoJogar()
    {
        menuInicial.SetActive(false);
        comoJogar.SetActive(true);
    }

    public void Voltar()
    {
        menuInicial.SetActive(true);
        comoJogar.SetActive(false);
    }
}

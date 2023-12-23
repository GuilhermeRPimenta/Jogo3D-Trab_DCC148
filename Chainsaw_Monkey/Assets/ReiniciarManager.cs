using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarManager : MonoBehaviour
{
    [SerializeField] private GameObject voceMorreu;
    [SerializeField] private GameObject botaoReiniciar;

    public void Morreu()
    {
        voceMorreu.SetActive(true);
        botaoReiniciar.SetActive(true);
    }

    public void Reiniciar()
    {
        Debug.Log("cu de macaco");
        SceneManager.LoadScene(1);
        // voceMorreu.SetActive(false);
        // botaoReiniciar.SetActive(false);
    }
}

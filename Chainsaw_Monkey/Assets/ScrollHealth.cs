using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHealth : MonoBehaviour
{
    public Sprite statusText;
    public Image colorBar;
    public float scrollSpeed;
    public Sprite[] healthStatus;
    public GameObject statusGameObject;
    public Image statusGameObjectImage;
    public PlayerScript playerScript;

    void Start()
    {
        colorBar = this.GetComponent<Image>();
        colorBar.color = new Color32(0,255,0,255);
        //statusGameObject.GetComponent<Image>().sprite = status;
        statusGameObjectImage = statusGameObject.GetComponent<Image>();
        statusText = healthStatus[0];

    }

    void Update()
    {
        statusGameObjectImage.sprite = statusText;
        colorBar.material.mainTextureOffset = colorBar.material.mainTextureOffset + new Vector2(Time.deltaTime * (-scrollSpeed / 10), 0);

        if(playerScript.HP >= 35){
            colorBar.color = new Color32(0,255,0,255);
            statusText = healthStatus[0];
        }
        else if(playerScript.HP < 35 && playerScript.HP >=15){
            colorBar.color = new Color32(255,255,0,255);
            statusText = healthStatus[2];
        }
        else{
            colorBar.color = new Color32(255,0,0,255);
            statusText = healthStatus[1];
        }
    }
}

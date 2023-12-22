using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    private bool running = false;
    public GameObject door;
    public Light generatorLight;
    public AudioSource sound;
    public AIController enemyAIController;
    public GameObject enemyAIHolder;

    void Start(){
        enemyAIHolder = GameObject.Find("AIControllerHolder");
        enemyAIController = enemyAIHolder.GetComponent<AIController>(); 
    }
    public void Activate()
    {
        if(running) return;

        running = true;
        enemyAIController.heardSound = true;
        enemyAIController.soundPosition = transform.position;
        generatorLight.color = Color.green;
        // sound.Play();
        
        GameObject[] generators = GameObject.FindGameObjectsWithTag("Generator");
        foreach (GameObject generator in generators){
            GeneratorScript generatorScript = generator.GetComponent<GeneratorScript>();
            if(!generatorScript.running){
                return;
            }
        }

        Debug.Log("Abriu");
        DoorScript doorScript = door.GetComponent<DoorScript>();
        doorScript.Open();

    }

}

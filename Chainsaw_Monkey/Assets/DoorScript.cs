using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public AudioClip openSound;
    private AudioSource sound;

    void Start(){
        sound = GetComponent<AudioSource>();
    }
    
    public void Open(){
        leftDoor.transform.Rotate(new Vector3(0, 90, 0));
        rightDoor.transform.Rotate(new Vector3(0, 90, 0));
        sound.clip = openSound;
        sound.Play();
    }
}

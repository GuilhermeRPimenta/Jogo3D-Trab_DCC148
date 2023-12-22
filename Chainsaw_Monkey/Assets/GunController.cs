using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public float bodyDamage = 10f;
    public float headDamage = 20f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem particleSystem;
    public AudioSource sound;
    public AIController enemyAIController;

    void Start()
    {
        particleSystem.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        particleSystem.Play();
        sound.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            //EnemyScript target = hit.transform.GetComponent<EnemyScript>();
            //Debug.Log(target);
            if (hit.transform.CompareTag("Enemy")){
                enemyAIController.HP -= bodyDamage;
                enemyAIController.SP -= bodyDamage;
            }
            else if(hit.transform.CompareTag("EnemyHead")){
                enemyAIController.HP -= headDamage;
                enemyAIController.SP -= headDamage;
            }
            
                
            
        }
    }
}
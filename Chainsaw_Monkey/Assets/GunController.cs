using UnityEngine;
using System.Collections;
using TMPro;

public class GunController : MonoBehaviour
{
    public float bodyDamage = 10f;
    public float headDamage = 20f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem particleSystem;
    public AudioSource[] sound;
    public AIController enemyAIController;
    public float shootTimer = 0.8f;
    public float shootDuration = 0.8f;
    public bool shot = false;
    public int magAmmo = 12;
    public int remainingAmmo = 84;
    public bool isReloading = false;
    public float reloadTimer = 2;
    public float reloadDuration = 2;
    public TextMeshProUGUI ammoDisplay;
    public AudioClip reloadAudio;
    public AudioClip shootAudio;

    void Start()
    {
        particleSystem.Stop();
        sound = GetComponentsInChildren<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
            shootTimer += Time.deltaTime;
            if(shootTimer >= shootDuration){
                shot = false;
                shootTimer = shootDuration;
            }
        
        if (Input.GetButtonDown("Fire1"))
        {
            if(shootTimer >= shootDuration && magAmmo >0 && !isReloading){
                Shoot();
                
                shootTimer = 0;
                shot = true;
                magAmmo -= 1;
            }
        }
        
        if(Input.GetKeyDown(KeyCode.R) && magAmmo !=12 && remainingAmmo !=0){
            isReloading = true;
            sound[0].clip = reloadAudio;
            sound[0].Play();
            int subtractedAmmo = 12 - magAmmo;
            if(remainingAmmo < subtractedAmmo ){
                magAmmo = remainingAmmo;
                remainingAmmo = 0;
            } 
            else{
                magAmmo = 12;
                remainingAmmo -= subtractedAmmo;
            }
            reloadTimer = 0;
        }

        if(isReloading){
            if(reloadTimer < reloadDuration){
                Reload();
            }
            else {
                reloadTimer = reloadDuration;
                isReloading = false;
            }
        }

        ammoDisplay.SetText($"{magAmmo} / {remainingAmmo}");

        if(Input.GetButtonDown("Interact")){
            Interact();
        }
    }

    void FixedUpdate(){
        
    }
    void Shoot()
    {
        particleSystem.Play();
        sound[0].clip = shootAudio;
        sound[0].Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
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

    void Reload(){
        Vector3 gunPos = transform.localPosition;
        if(reloadTimer < reloadDuration/1.5f){
            gunPos.y -= 3 * Time.deltaTime;
            if(gunPos.y < -1){
                gunPos. y = -1;
            }
        }
        else{
            gunPos.y += 3 * Time.deltaTime;
            if(gunPos.y > -0.07f)
            {
                gunPos.y = -0.07f;
            }
        }
        
        transform.localPosition = gunPos;
        reloadTimer += Time.deltaTime;
    }
    
    void Interact()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            GeneratorScript target = hit.transform.GetComponent<GeneratorScript>();
            if (target != null)
            {
                target.Activate();
            }
        }
    }

}
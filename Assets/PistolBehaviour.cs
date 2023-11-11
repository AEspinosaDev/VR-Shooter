using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(LineRenderer))]
public class PistolBehaviour : MonoBehaviour
{
    [SerializeField] protected float shootingForce;
    [SerializeField] private float recoilForce;
    [SerializeField] private float damage;
    [SerializeField] private Transform dazzle;

    private LineRenderer lineRenderer;
    private float timer;
    public float lifetime = 1.0f;
    public float scaleDownFactor = 0.95f;

    private Rigidbody rigidbody;
    private XRGrabInteractable interactableWeapon;

    [SerializeField] private AudioClip shootingSound; // Assign this in the inspector
    [SerializeField] private float soundLifetime = 1.0f;

    protected virtual void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        SetupInteractableWeaponEvents();
    }

    private void Update()
    {
        if (timer > 0)
        {
            // Scale down the ray
            lineRenderer.startWidth *= scaleDownFactor;
            lineRenderer.endWidth *= scaleDownFactor;

            timer -= Time.deltaTime;
        }
        else {
            lineRenderer.enabled = false;
        }
    }

    private void SetupInteractableWeaponEvents()
    {
        interactableWeapon.onSelectEntered.AddListener(PickUpWeapon);
        interactableWeapon.onSelectExited.AddListener(DropWeapon);
        interactableWeapon.onActivate.AddListener(StartShooting);
        interactableWeapon.onDeactivate.AddListener(StopShooting);
    }

    private void PickUpWeapon(XRBaseInteractor interactor)
    {
        Debug.Log("Weapon picked");
    }

    private void DropWeapon(XRBaseInteractor arg0)
    {
        Debug.Log("Weapon dropped");
    }

    private void StartShooting(XRBaseInteractor arg0)
    {
        Shoot();
    }

    private void StopShooting(XRBaseInteractor arg0)
    {
        
    }

    public void Shoot()
    {
        ApplyRecoil();
        lineRenderer.enabled = true;

        RaycastHit hit;
        Vector3 start = dazzle.position;
        Vector3 direction = transform.forward;
        Vector3 end = start + direction * 10;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        timer = lifetime;


        if (Physics.Raycast(start, direction, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "enemy") { 
                
                Debug.Log("Hit enemy");
            }
            Debug.DrawRay(start, direction * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(start, direction * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

        PlayShootingSound();
    }

    private void ApplyRecoil()
    {
        rigidbody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
    }

    public float GetShootingForce()
    {
        return shootingForce;
    }

    public float GetDamage()
    {
        return damage;
    }


    private void PlayShootingSound()
    {
        GameObject audioObject = new GameObject("ShootingSound");
        audioObject.transform.position = transform.position; // Set the position to the weapon's position

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = shootingSound;
        audioSource.playOnAwake = false;

        audioSource.Play();

        Destroy(audioObject, soundLifetime);
    }

}

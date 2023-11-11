using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Unity.VisualScripting;

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


    int max_ammo = 10;
    int current_ammo = 0;
    [SerializeField] private AudioClip reload_sound;
    [SerializeField] private AudioClip no_ammo_sound;
    [SerializeField] private TextMeshPro ammo_text;

    [SerializeField] private Light light;

    protected virtual void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        current_ammo = max_ammo;
        ammo_text.text = current_ammo.ToString();
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


        // Reload
        if (Vector3.Angle(transform.up, Vector3.up) > 100 && current_ammo < max_ammo) Reload();
    }

    public void Shoot()
    {

        if (current_ammo > 0)
        {
            --current_ammo;

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
                //Debug.DrawRay(start, direction * hit.distance, Color.yellow);

                if (hit.collider.tag == "enemy")
                {
                    //Debug.Log("Hit enemy");
                    hit.collider.gameObject.GetComponent<Zombie_behaviour>().ReceiveDamage((int)damage);
                }
            }

            PlayShootingSound();


            ammo_text.text = current_ammo.ToString();

            light.enabled = true;

            StartCoroutine(Wait());
        }
        else {
            PlayEmpty();
            lineRenderer.enabled = false;
        }
        
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
        audioObject.transform.position = transform.position;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = shootingSound;
        audioSource.playOnAwake = false;

        audioSource.Play();

        Destroy(audioObject, soundLifetime);
    }

    private void Reload() {
        GameObject audioObject = new GameObject("ReloadingSound");
        audioObject.transform.position = transform.position;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = reload_sound;
        audioSource.playOnAwake = false;

        audioSource.Play();

        Destroy(audioObject, soundLifetime);

        current_ammo = max_ammo;
        ammo_text.text = current_ammo.ToString();
    }

    private void PlayEmpty() {
        GameObject audioObject = new GameObject("EmptySound");
        audioObject.transform.position = transform.position;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = no_ammo_sound;
        audioSource.playOnAwake = false;

        audioSource.Play();

        Destroy(audioObject, soundLifetime);
    }


    IEnumerator Wait()
    {

        yield return new WaitForSeconds(.03f);
        light.enabled = false;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    private AudioSource weaponAudio;
    public AudioClip weaponAudioClip;

    public MeshRenderer muzzleFlash;
    public Transform bulletTrail;


    public float fireRate = 0.08f;
    private float nextFire = 0.0f;

    Transform CameraTransform;
    EnemyDamage enemyDamage;

    private Animator weaponAnimator;
    private Vector3 originalWeaponPosition;


    private void Start()
    {

        muzzleFlash.enabled = false;

        weaponAudio = GetComponent<AudioSource>();
        weaponAnimator = GetComponent<Animator>();
        enemyDamage = GetComponent<EnemyDamage>();
        CameraTransform = Camera.main.transform;

        originalWeaponPosition = this.transform.localPosition;


    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(Time.time >= nextFire){
                nextFire = Time.time + fireRate;

                StartCoroutine(FireEffect());

            }
            float z = Random.Range(0, 2.0f);
            bulletTrail.localScale = new Vector3(bulletTrail.localScale.x, bulletTrail.localScale.y, z);

            RaycastHit hit;
            if (Physics.Raycast(CameraTransform.position,CameraTransform.forward, out hit, 10.0f))
            {
                if(hit.collider.tag == "ENEMY")
                {
                    object[] _params = new object[2];
                    _params[0] = hit.point;
                    _params[1] = 20;

                    hit.collider.gameObject.SendMessage("EnemyHitDamage", _params, SendMessageOptions.DontRequireReceiver);
                }
                if(hit.collider.tag == "TARGET")
                {
                    hit.collider.gameObject.SendMessage("TargetDamage");
                }
            }

        }
        else
        {
            bulletTrail.localScale = new Vector3(bulletTrail.localScale.x, bulletTrail.localScale.y, 0.0f);
        }

    }

    IEnumerator FireEffect()
    {
        weaponAnimator.enabled = false;
        Vector2 randomFireWeaponPosition = Random.insideUnitCircle;
        this.transform.localPosition += new Vector3(0, randomFireWeaponPosition.x * 0.01f, randomFireWeaponPosition.y * 0.01f);

        muzzleFlash.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));
        muzzleFlash.enabled = true;
        weaponAudio.PlayOneShot(weaponAudioClip);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        muzzleFlash.enabled = false;

        this.transform.localPosition = originalWeaponPosition;
    }
}

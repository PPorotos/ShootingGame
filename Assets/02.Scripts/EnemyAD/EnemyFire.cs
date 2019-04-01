using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    private Transform playerTransform;
    private Transform enemyTransform;


    private readonly int hashFire = Animator.StringToHash("Fire");
    private readonly int hashreload = Animator.StringToHash("Reload");

    private float nextFire = 0.0f;

    private readonly float fireRate = 0.1f;
    private readonly float damping = 10.0f;

    private readonly float reloadTime = 2.0f;
    private readonly int maxbullet = 10;
    private int currBullet = 10;
    private bool isReload = false;

    private WaitForSeconds wsReload;

    public bool isFire = false;

    public AudioClip fireSfx;
    public AudioClip reloadSfx;

    public GameObject Bullet;
    public Transform firePosition;

    public MeshRenderer muzzleFlash;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTransform = transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        wsReload = new WaitForSeconds(reloadTime);

        muzzleFlash.enabled = false;
    }
    private void Update()
    {
        if (!isReload && isFire)
        {
            if(Time.time >= nextFire)
            {
                Fire();

                nextFire = Time.time + fireRate + Random.Range(0.0f, 0.3f);
            }
            Quaternion rotation = Quaternion.LookRotation(playerTransform.position - enemyTransform.position);
            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, rotation, Time.deltaTime * damping);
        }
    }
    void Fire()
    {
        animator.SetTrigger(hashFire);
        audioSource.PlayOneShot(fireSfx, 1.0f);

        StartCoroutine(ShowMuzzleFlash());

        GameObject _bullet = Instantiate(Bullet, firePosition.position, firePosition.rotation);
        Destroy(_bullet, 1.0f);

        isReload = (--currBullet % maxbullet == 0);
        if (isReload)
        {
            StartCoroutine(Reloading());
        }
    }
    IEnumerator Reloading()
    {
        muzzleFlash.enabled = false;
        animator.SetTrigger(hashreload);
        audioSource.PlayOneShot(reloadSfx, 1.0f);

        yield return wsReload;

        currBullet = maxbullet;
        isReload = false;
    }
    IEnumerator ShowMuzzleFlash()
    {
        muzzleFlash.enabled = true;

        Quaternion imageRotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
        muzzleFlash.transform.localRotation = imageRotation;
        muzzleFlash.transform.localScale = Vector3.one * Random.Range(0.3f, 0.5f);

        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.SetTextureOffset("_MainTex", offset);

        yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));

        muzzleFlash.enabled = false;
    }
}

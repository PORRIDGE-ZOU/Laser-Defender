using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float fireRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI = false;
    [SerializeField] float firingVariance = 0.7f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI){isFiring = true;}
    }


    void Update()
    {

        Fire();

    }


    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }



    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab,
                                                transform.position,
                                                Quaternion.identity);
            Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();
            rigidbody.velocity = transform.up * projectileSpeed;
            Destroy(projectile, projectileLifetime);
            float timeToNextFire = Random.Range(fireRate - firingVariance,
                                            fireRate + firingVariance);
            timeToNextFire = Mathf.Clamp(timeToNextFire,
                                        minimumFiringRate, float.MaxValue);
            audioPlayer.PlayShootingClip();

            if (useAI) { yield return new WaitForSeconds(timeToNextFire); }
            else { yield return new WaitForSeconds(fireRate); }
            
        }
    }

}

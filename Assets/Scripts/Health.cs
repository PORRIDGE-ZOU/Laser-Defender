using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    CameraShake cameraShake;
    [SerializeField] bool applyCameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager; 

    private void Awake()
    {
        if (applyCameraShake)
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
        }
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {

            PlayHitEffect();
            damageDealer.Hit();
            if(cameraShake != null)
            {
                cameraShake.Play();
            }
            TakeDamage(damageDealer.GetDamageValue());

        }

    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            if (!isPlayer)
            {
                Debug.Log("adding score!");
                scoreKeeper.AddScore(score);
            }
            if (isPlayer)
            {
                Debug.Log("game is now over.");
                levelManager.LoadGameOver();
            }
            Destroy(gameObject);

        }
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect,
                                                  transform.position,
                                                  Quaternion.identity);
            Destroy(instance.gameObject,
                    instance.main.duration + instance.main.startLifetime.constantMax);

        }
        if(audioPlayer != null)
        {
            audioPlayer.PlayDamageClip();
        }
    }

    public int GetHealth()
    {
        return health;
    }



}

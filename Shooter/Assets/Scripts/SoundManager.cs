using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //creates the soundmanager instance
    public static SoundManager Instance { get { return _instance; } }
    private static SoundManager _instance;

    [SerializeField] private AudioSource PlayerAudioSource;
    [SerializeField] private AudioSource EnemyAudioSource;
    private GameObject playerObject;

    void Awake()
    {
        //makes sure there is only one soundmanager instance and sets that instance to this
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void FixedUpdate()
    {
        playerObject = GameObject.FindObjectOfType<PlayerDamage>().gameObject;

        if (playerObject != null)
        {
            transform.position = playerObject.transform.position;
        }
    }

    public void PlaySound(bool isPlayer, AudioClip audioClip)
    {
        AudioSource audioSource = isPlayer ? PlayerAudioSource : EnemyAudioSource;

        //play sound if not already playing that same sound, prevents stacking sounds and earrape
        if (!(audioSource.isPlaying && audioSource.clip == audioClip)) 
        { 
            audioSource.clip = audioClip; 
            audioSource.Play(); 
        }
    }
}

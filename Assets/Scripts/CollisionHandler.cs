using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // PARAMS
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    // CACHE
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }    
    }

    void StartSuccessSequence()
    {
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void LoadNextLevel()
    { 
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }  
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    float delayTimer = 1f;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondDebugKeys();
    }
    void RespondDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
            collisionDisable = !collisionDisable;

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            LoadPreviousLevel();

        else if (Input.GetKeyDown(KeyCode.RightArrow))
            LoadNextLevel();
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisable) 
            return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //Launching pad, will implement new features soon.
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadLevel", delayTimer);
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("LoadNextLevel", delayTimer);
    }
 
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        // Load level 1 if last scene is finished.
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
    }
    void LoadPreviousLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousSceneIndex = currentSceneIndex - 1;

        if (currentSceneIndex != 0)
            SceneManager.LoadScene(previousSceneIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;



public class CollisionDetector : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int Score {  get; private set; }

    private int maxHealth = 5;
    private int currentHealth;
    
    public UnityEngine.UI.Slider slider;
    public Gradient gradient;
    public UnityEngine.UI.Image fill;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text HighscoreText;
    public static bool PlayerDead = false;

    public static HighScoreManager instance;
    public GameObject Explosion;
    

    public GameOverScreen gameoverScreen;
    public static bool GameIsPaused = false;
    public PauseMenu pauseMenu;

    public GameObject bloodsplatter;

    


    void Start()
    {
        setMaxHealth(maxHealth);
        Score = 0;
        PlayerDead = false;
        AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Music_Menu);
        if (HighScoreManager.instance != null)
        {
            HighscoreText.text = "HighScore: " + HighScoreManager.instance.getHighScore();
        }
        else
        {
            HighscoreText.text = "Highscore not found";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerDead)
        {
            TogglePause();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) 
        {
            if (SpeedBoostManager.instance.IsBoostActive()) 
            {
                Destroy(other.gameObject);
            }
            else 
            {
                AudioManager.Instance.Play(AudioManager.SoundType.Obstacle);
                Debug.Log("Hit Obstacle");
                GameObject fx = Instantiate(Explosion, other.transform.position, transform.rotation);
                StartCoroutine(DestroyWhenDone(fx));
                TakeDamage(1);
            }

            
        }

        if (other.gameObject.CompareTag("Zombie"))
        {
            
            Debug.Log("Hit Zombie");
            AddScore(1);
            AudioManager.Instance.Play(AudioManager.SoundType.Hit);
            Zombie zombie  = other.gameObject.GetComponent<Zombie>();
            Vector3 force = new Vector3(0, 900f, 0f);
            Vector3 hitPoint = zombie.transform.position + Vector3.up * 3f;

            GameObject fx =  Instantiate(bloodsplatter, other.transform.position, transform.rotation);
            StartCoroutine(DestroyWhenDone(fx));
            


            zombie.TriggerRagdoll(force, hitPoint);
            
        }
    }

    public int getScore() 
    {
        return Score;
    }

    public void AddScore(int amount) 
    {
        Score += amount;
        scoreText.text = "Score: " + Score.ToString();

    }

    public void TakeDamage(int damage) 
    { 
        currentHealth -= damage;
        slider.value = currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        
        if (currentHealth <= 0) 
        {
            AudioManager.Instance.PauseMusic();
            
            PlayerDead = true;
            
            if (HighScoreManager.instance != null) 
            {
                HighScoreManager.instance.SaveHighScore(Score);
            }
            gameoverScreen.Setup(Score);
            Time.timeScale = 0f;
            
        }
    }
    public void setHealth(int health) 
    {
        slider.value = health;
        float gradientNum = (float)health/(float)maxHealth;
        fill.color = gradient.Evaluate(gradientNum);
    }

    public void setMaxHealth(int health) 
    {
        currentHealth = health;
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
        Debug.Log("Max Health changed to : " + slider.maxValue);
    }

    public void Heal(int amount) 
    {
        Debug.Log("Heal");
        if (currentHealth >= maxHealth && maxHealth <= 7)
        {
            setMaxHealth(currentHealth + amount);
            return;
        }
        else 
        {
            currentHealth += amount;
            setHealth(currentHealth);
            Debug.Log(currentHealth);

        }
    }

    void TogglePause() 
    {
        if (GameIsPaused)
        {
            pauseMenu.ResumeButton();
        }
        else 
        {
            AudioManager.Instance.PauseMusic();
            pauseMenu.Setup(Score, HighScoreManager.instance.getHighScore());
        }
    }

    IEnumerator DestroyWhenDone(GameObject fx)
    {
        ParticleSystem ps = fx.GetComponent<ParticleSystem>();

        yield return new WaitUntil(() => !ps.IsAlive(true));

        Destroy(fx);
    }



}

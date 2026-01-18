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
                Debug.Log("Hit Obstacle");
                Instantiate(Explosion, other.transform.position, transform.rotation);
                TakeDamage(1);
            }

            
        }

        if (other.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Hit Zombie");
            AddScore(1);
            Zombie zombie  = other.gameObject.GetComponent<Zombie>();
            Vector3 force = new Vector3(0, 900f, 0f);
            Vector3 hitPoint = zombie.transform.position + Vector3.up * 3f;

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

        // Show blood splatter and fade out
        if (bloodsplatter != null)
        {
            Image bloodImage = bloodsplatter.GetComponent<Image>();
            if (bloodImage != null)
            {
                StopCoroutine("BloodFade"); // Stop previous fade if running
                StartCoroutine(BloodFade(bloodImage, 2f)); // 2-second fade
            }
        }
    }

    public void TakeDamage(int damage) 
    { 
        currentHealth -= damage;
        slider.value = currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        
        if (currentHealth <= 0) 
        {
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
            pauseMenu.Setup(Score, HighScoreManager.instance.getHighScore());
        }
    }

    private IEnumerator BloodFade(UnityEngine.UI.Image img, float duration)
    {
        float elapsed = 0f;

        // Make fully visible
        Color c = img.color;
        c.a = 1f;
        img.color = c;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            c.a = alpha;
            img.color = c;
            yield return null;
        }

        // Ensure fully transparent at the end
        c.a = 0f;
        img.color = c;
    }

}

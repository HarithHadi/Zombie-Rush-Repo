using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


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


    public static HighScoreManager instance;
    public GameObject Explosion;

    public GameOverScreen gameoverScreen;



    void Start()
    {
        setMaxHealth(maxHealth);
        Score = 0;
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
    }

    public void TakeDamage(int damage) 
    { 
        currentHealth -= damage;
        slider.value = currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        
        if (currentHealth <= 0) 
        {
            Debug.Log("Player Dead");
            
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

    
}

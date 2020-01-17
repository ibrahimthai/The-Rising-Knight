using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* This class prepares and updates the number of lives for the user to play the game.
 * This is also responsible for the Game Over window page */
public class Health : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject gameOverText;
    public GameObject gameOverScoreText;
    public GameObject gameOverScoreNum;
        
    public Text newScore;
    int oldScore = int.Parse(ButtonListControl.myUsersCurrentHighscore);

    // Health bar
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    // Energy bar
    public float CurrentEnergy { get; set; }
    public float MaxEnergy { get; set; }

    public static float damageKey = 0;
    public static float energyKey = 0;
    
    public Slider healthBar;
    public Text currentHealthPoints;

    public Slider energyBar;
    public Text currentEnergyPoints;
    
    private void Start()
    {
        // Health starts and resets to full when starting game again
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;
        healthBar.value = MaxHealth;
        Debug.Log("Health is " + healthBar.value);
        currentHealthPoints.text = MaxHealth.ToString();

        // Energy starts and resets to 0 when starting game again
        MaxEnergy = 0f;
        CurrentEnergy = MaxEnergy;
        energyBar.value = MaxEnergy;
        Debug.Log("Health is " + healthBar.value);
        currentEnergyPoints.text = MaxEnergy.ToString();

        ScoreScript.scoreValue = 0;
        restartButton.SetActive(false);
        exitButton.SetActive(false);
        gameOverText.SetActive(false);
        gameOverScoreText.SetActive(false);
        gameOverScoreNum.SetActive(false);

    }

    private void Update()
    {
        // Damage numbers
        if (damageKey > 0)
        {
            // Key 1: Health Potion
            if (damageKey == 1)
                HealDamage(20);
            // Key 10: Frogs
            else if (damageKey == 10)
                DealDamage(10);
            // Key 20: Eagles, Bats, and Fire Tiles
            else if (damageKey == 20)
                DealDamage(20);

            // Resets the key after player hits an object again
            damageKey = 0;
        }

        // Energy Numbers
        if (energyKey > 0)
        {
            // Key 1: Energy Potion
            if (energyKey == 1)
                HealEnergy(5);

            // Resets the key after player hits an object again
            energyKey = 0;
        }

    }

    void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthBar.value = CurrentHealth;
        Debug.Log("Your health is " + healthBar.value);
        currentHealthPoints.text = healthBar.value.ToString();

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Debug.Log("You died");

            restartButton.SetActive(true);
            exitButton.SetActive(true);
            gameOverText.SetActive(true);
            gameOverScoreText.SetActive(true);
            gameOverScoreNum.SetActive(true);
            ScoreCalculation();
            Time.timeScale = 0;
        }
    }

    void HealDamage(float healValue)
    {
        CurrentHealth += healValue;
        healthBar.value = CurrentHealth;
        Debug.Log("Heal Value: " + healValue);
        Debug.Log("Your health is " + healthBar.value);
        currentHealthPoints.text = healthBar.value.ToString();

        if (CurrentHealth >= 100)
            CurrentHealth = 100;
    }

    void HealEnergy(float healValue)
    {
        CurrentEnergy += healValue;
        energyBar.value = CurrentEnergy;
        Debug.Log("Heal Value: " + healValue);
        Debug.Log("Your health is " + energyBar.value);
        currentEnergyPoints.text = energyBar.value.ToString();

        if (CurrentEnergy >= 100)
            CurrentEnergy = 100;
    }

    void ScoreCalculation()
    {
        if (int.Parse(newScore.text) > oldScore)
        {
            StartCoroutine(UpdateHighscore(ButtonListControl.myChosenUsername, newScore.text, StageListMenuButtons.myChosenStage));
            enabled = false;
        }
    }

    /* Updates the highscore on the database once user dies */
    IEnumerator UpdateHighscore(string username, string highscore, string stage)
    {
        string data;
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("highscore", highscore);
        form.AddField("stage", stage);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/updateUsersHighscore.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error SQL connection");
        }
        else
        {
            data = www.downloadHandler.text;
            Debug.Log(stage + " score updated successfully: " + username + "|" + data);
        }
    }


}

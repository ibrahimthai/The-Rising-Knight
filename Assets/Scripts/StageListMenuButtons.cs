using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageListMenuButtons : MonoBehaviour
{
    // The stage you chose to play on
    public static string myChosenStage;

    [SerializeField]
    private Text myDisplayedStageText;

    [SerializeField]
    private Text myDisplayedHighscore;

    [SerializeField]
    private Text myDisplayedDifficultyLevelText;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Image plains;

    [SerializeField]
    private Image darkForest; 

    // Disables the buttons until the user clicks on a username on the list
    void Start()
    {
        startButton.interactable = false;
        plains.enabled = false;
        darkForest.enabled = false;
    }

    // Takes you to the stage you chose
    public void GoToSpecificStage()
    {
        if (myDisplayedStageText.text == "Plains")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        else if (myDisplayedStageText.text == "Dark Forest")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
        }
    }

    public void DisplayPlainsStage()
    {
        myChosenStage = "Plains";
        startButton.interactable = true;
        myDisplayedStageText.text = "Plains";
        myDisplayedDifficultyLevelText.text = "Easy";
        plains.enabled = true;
        darkForest.enabled = false;

        StartCoroutine(RetrieveUserScore(ButtonListControl.myChosenUsername, myDisplayedStageText.text));
    }
    
    public void DisplayDarkForestStage()
    {
        myChosenStage = "Dark Forest";
        startButton.interactable = true;
        myDisplayedStageText.text = "Dark Forest";
        myDisplayedDifficultyLevelText.text = "Hard";
        darkForest.enabled = true;
        plains.enabled = false;

        StartCoroutine(RetrieveUserScore(ButtonListControl.myChosenUsername, myDisplayedStageText.text));
    }

    /* This function retrieves the users highscore and displays it */
    IEnumerator RetrieveUserScore(string username, string stage)
    {
        string highscore;
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("stage", stage);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/getUserScore.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error SQL connection");
        }
        else
        {
            highscore = www.downloadHandler.text;
            Debug.Log("Highscore: " + highscore);
            //Debug.Log("Highscore: " + stage);
            myDisplayedHighscore.text = highscore;
        }

    } // End of RetrieveUserScore()




}

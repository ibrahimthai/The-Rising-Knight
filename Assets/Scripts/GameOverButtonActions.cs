using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* This class deals with Game Over functionalities after the user loses during the gameplay */
public class GameOverButtonActions : MonoBehaviour
{
    public Text newUserScore;
    int oldScore = int.Parse(ButtonListControl.myUsersCurrentHighscore);

    /* If user clicks on Retry, the Game Screen scene resets and starts over fresh */
    public void RetryAgain()
    {
        if (int.Parse(newUserScore.text) > oldScore)
        {
            StartCoroutine(UpdateRetry(ButtonListControl.myChosenUsername, newUserScore.text));
        }
        else
        {
            SceneManager.LoadScene("GameScreen");
            Debug.Log("Score NOT updated");
        }
    }

    /* If user decides to exit gameplay, the scores are evaluated.
     * If user gets a high score, the score is updated on the database*/
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    /* Updates the highscore on the database */

    IEnumerator UpdateRetry(string username, string highscore)
    {
        string data;
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("highscore", highscore);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/updateUsersHighscore.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error SQL connection");
        }
        else
        {
            data = www.downloadHandler.text;
            Debug.Log("Score updated successfully: " + username + "|" + data);
            SceneManager.LoadScene("GameScreen");
        }
    }
}

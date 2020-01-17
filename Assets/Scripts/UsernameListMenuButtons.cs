using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UsernameListMenuButtons : MonoBehaviour
{
    [SerializeField]
    public Text usernameToRemove;

    public void PlayToStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Username chosen: " + ButtonListControl.myChosenUsername);     
    }

    public void PlayToGoStageList()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GoToAddUsername()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void GoToEditUsername()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void CallRemoveUsername()
    {
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameToRemove.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/removeUsername.php", form);
        yield return www.SendWebRequest();

        Debug.Log("Username removed successfully.");
        SceneManager.LoadScene("UsernameListMenu");
    }




}
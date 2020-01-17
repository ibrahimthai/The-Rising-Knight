using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* This class deals with handling usernames that users want to edit, but want to keep their scores*/
public class EditUsernameActions : MonoBehaviour
{
    public InputField usernameEditField;
    public Button cancelButton;
    public Button editButton;
    private string newUsername;

    /* Display the current username on the inputfield */
    private void Start()
    {
        usernameEditField.text = ButtonListControl.myChosenUsername;
    }

    /* Once user clicks Change, the username is edited */
    public void CallEditUsername()
    {
        StartCoroutine(Edit());
    }

    /* Calls out SQL command to update the username */
    IEnumerator Edit()
    {
        string data;
        newUsername = usernameEditField.text;
        WWWForm form = new WWWForm();
        form.AddField("username", ButtonListControl.myChosenUsername);
        form.AddField("newUsername", newUsername);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/editUsername.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error SQL connection");
        }
        else
        {
            data = www.downloadHandler.text;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        }
    }

    /* If user doesn't want to edit and goes back to the username list */
    public void CancelEditUsername()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

}

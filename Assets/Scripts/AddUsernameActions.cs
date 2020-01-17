using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* This class handles allowing the user to add his/her own unique username for themselves */
public class AddUsernameActions : MonoBehaviour
{
    public InputField usernameInputField;
    public Button cancelButton;
    public Button addButton;
    
    /* Once user clicks Add, a username is created */
    public void CallAddUsername()
    {
        StartCoroutine(Add());
    }

    /* Calls out SQL command to update the username */
    IEnumerator Add()
    {
        // Creates and adds the username to the form that will be sent to the PHP file
        WWWForm form = new WWWForm();
        form.AddField("username", usernameInputField.text);

        // Sends the username to the PHP file to call out SQL command
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/userinfo.php", form);
        yield return www.SendWebRequest();

        Debug.Log("Username added successfully.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    /* Checks to see if user inputs at least 1 character */
    public void VerifyInputs()
    {
        addButton.interactable = (usernameInputField.text.Length > 0);
    }

    /* If user doesn't want to edit and goes back to the username list */
    public void CancelAddUsername()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

}

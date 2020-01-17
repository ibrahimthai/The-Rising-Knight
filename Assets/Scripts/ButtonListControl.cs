using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/* This class creates and displays the list of usernames in an organized fashion.
 * It also includes the functionality of the username buttons */
public class ButtonListControl : MonoBehaviour
{
    public static string myChosenUsername;
    public static string myUsersCurrentHighscore;

    [SerializeField]
    private GameObject buttonTemplate;
    
    // Holds the list of usernames in the database
    public string[] items;

    [SerializeField]
    private Text myDisplayedText;

    [SerializeField]
    private Button removeButton;

    [SerializeField]
    private Button editButton;

    [SerializeField]
    private Button playButton;

    [SerializeField]
    public static string sendUsernameToEdit;

    [SerializeField]
    public static EditUsernameActions editUsername;

    // Disables the buttons until the user clicks on a username on the list
    void Start()
    {
        StartCoroutine(RetrieveUsernameSize());
        removeButton.interactable = false;
        editButton.interactable = false;
        playButton.interactable = false;
    }

    /* Function allows the game to get the number of usernames in the SQL database */
    IEnumerator RetrieveUsernameSize()
    {
        string uri = "http://localhost/sqlconnect/getNumberOfUsernames.php";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            string dataInString;
            int result = 0;
            int page = pages.Length - 1;

            // Check if we have access to userinfo database
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                // Display the total number of usernames in database
                dataInString = webRequest.downloadHandler.text;

                // Convert the String to Int
                int.TryParse(dataInString, out result);
                Debug.Log("Size: " + result);
                GenerateList(result);
            }
        }

    } // End of RetrieveUsernameSize()

    /* Creates a list of buttons based on the size of the SQL database */
    public void GenerateList(int size)
    {
        // Create list of buttons based on the size of the database
        for (int i = 1; i <= size; i++)
        {
            StartCoroutine(RetrieveUsername(i));
        }

    } // End of GenerateList()


    /* This function retrieves all the username in the database and displays it */
    IEnumerator RetrieveUsername(int index)
    {
        string uri = "http://localhost/sqlconnect/displayUsername.php";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            string dataInString;
            int page = pages.Length - 1;

            // Check if we have access to userinfo database
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                // Creates a new Button dynamically
                GameObject buttonToName = Instantiate(buttonTemplate) as GameObject;
                buttonToName.SetActive(true);

                // Fixes Null Exception Error
                if (buttonToName != null)
                {
                    // Get the usernames based on SQL command results
                    dataInString = webRequest.downloadHandler.text;

                    // Split the usernames based on the colon
                    items = dataInString.Split(new string[] { ":" }, StringSplitOptions.None);

                    // Set text of the button by their usernames
                    buttonToName.GetComponent<ButtonListButton>().SetText(items[index - 1]);
                    buttonToName.transform.SetParent(buttonTemplate.transform.parent, false);

                }
                    
            }
        }

    } // End of RetrieveUsername()

    /* Displays the username on the username board.
     * It also enables all the buttons and their functionalities */
    public void ButtonClicked(string nameDisplay)
    {
        GameObject editButtonToName = Instantiate(buttonTemplate) as GameObject;
        Debug.Log("Name Clicked: " + nameDisplay);
        myDisplayedText.text = nameDisplay;
        myChosenUsername = nameDisplay;
        removeButton.interactable = true;
        editButton.interactable = true;
        playButton.interactable = true;
    }

    

}

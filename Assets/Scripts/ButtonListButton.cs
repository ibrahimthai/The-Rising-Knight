using UnityEngine;
using UnityEngine.UI;

/* This class allows the users to see the username on each button list */
public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;

    [SerializeField]
    private ButtonListControl buttonToDisplayName;

    private string textToDisplay;

    /* Sets each username to the button and the  */
    public void SetText(string textString)
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        textToDisplay = textString;
        myText.text = textString;
    }

    /* Displays the clicked username on the display board */
    public void OnClick()
    {
        buttonToDisplayName.ButtonClicked(textToDisplay);
    }

}

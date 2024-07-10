using UnityEngine;

public class UIStartMenu : MonoBehaviour
{

    [SerializeField] private UIYesNoPopup yesNoPopUp;
    [SerializeField] private UIStartGameButton startGameButton;
    public GameObject ButtonHolder;


    private void Awake()
    {
        yesNoPopUp.gameObject.SetActive(false);

        startGameButton.gameObject.SetActive(true);


        startGameButton.exitButton.onClick.AddListener(ShowExitYesNoPopUp);
    }

    private void ShowExitYesNoPopUp()
    {
        yesNoPopUp.Show("You want to exit !!!");
    }
}
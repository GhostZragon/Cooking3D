using UnityEngine;

public class UIStartMenu : MonoBehaviour
{

    [SerializeField] private UIYesNoPopup yesNoPopUp;
    [SerializeField] private UIStartGameButton startGameButton;
    public GameObject ButtonHolder;


    private void Start()
    {
        yesNoPopUp.gameObject.SetActive(false);

        startGameButton.gameObject.SetActive(true);

        startGameButton.exitButton.btn.onClick.AddListener(ShowExitYesNoPopUp);
        startGameButton.ScaleBtn(Vector3.zero, 0, 0);
        Invoke(nameof(Test), 1f);
    }
    private void Test()
    {
        startGameButton.ScaleBtn(Vector3.one, .25f, .1f);
    }
    private void ShowExitYesNoPopUp()
    {
        yesNoPopUp.Show("You want to exit !!!");
    }


}


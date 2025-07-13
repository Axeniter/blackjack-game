using UnityEngine;

public class MovesHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Player player;
    [SerializeField] private GameObject SplitButton;
    [SerializeField] private GameObject DoubleButton;

    private bool CheckDouble() => gameManager.Bet <= player.Chips && player.Cards.Count == 2;
    private bool CheckSplit() => player.Cards.Count == 2 && player.Cards[0].Value == player.Cards[1].Value;

    private void OnEnable()
    {
        SplitButton.SetActive(CheckSplit());
        DoubleButton.SetActive(CheckDouble());
    }

}

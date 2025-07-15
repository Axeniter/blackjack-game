using UnityEngine;

public class MovesHandler : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject SplitButton;
    [SerializeField] private GameObject DoubleButton;

    private bool CheckDouble() => player.CurrentHand.Bet <= player.Chips && player.CurrentHand.Cards.Count <= 2;
    private bool CheckSplit() => player.CurrentHand.Cards.Count == 2 
        && player.CurrentHand.Cards[0].GetComponent<Card>().Value == player.CurrentHand.Cards[1].GetComponent<Card>().Value
        && !player.Splited && player.CurrentHand.Bet <= player.Chips;

    private void OnEnable()
    {
        SplitButton.SetActive(CheckSplit());
        DoubleButton.SetActive(CheckDouble());
    }

}

using TMPro;
using UnityEngine;

public class BetShower : MonoBehaviour
{
    [SerializeField] private TMP_Text betText;
    private int currentBet;
    void OnEnable()
    {
        currentBet = 0;
    }
    public void ChangeBetText(int bet)
    {
        currentBet += bet;
        betText.text = "Ставка: " + currentBet;
    }
}

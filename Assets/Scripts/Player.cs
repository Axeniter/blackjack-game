using UnityEngine;
using UnityEngine.Events;

public class Player : Person
{
    [SerializeField] private int chips;
    public int Chips => chips;
    public UnityEvent<int> OnBet;

    public void Bet(int bet)
    {
        chips -= bet;
        OnBet?.Invoke(bet);
    }
}

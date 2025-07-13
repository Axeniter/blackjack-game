using UnityEngine;
using UnityEngine.Events;

public class Player : Person
{
    [field: SerializeField] public int Chips { get; set; }

    public UnityEvent<int> OnBet;
    public UnityEvent<int> OnDouble;
    public UnityEvent<int> OnSplit;

    public void Bet(int bet)
    {
        Chips -= bet;
        OnBet?.Invoke(bet);
    }
}

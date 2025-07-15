using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Person
{
    [field: SerializeField] public int Chips { get; private set; }

    public UnityEvent<int> OnBet;
    public UnityEvent OnSplit;
    public UnityEvent OnTurn;
    public UnityEvent OnStand;

    public Hand SplitedHand1 { get; private set; } = null;
    public Hand SplitedHand2 { get; private set; } = null;
    public bool Splited { get; private set; } = false;

    public void Bet(int bet)
    {
        Chips -= bet;
        OnBet?.Invoke(bet);
    }
    
    public void Split()
    {
        Bet(CurrentHand.Bet);
        SplitedHand1 = MakeHand(CurrentHand.Bet, new List<GameObject> { CurrentHand.Cards[0] });
        SplitedHand2 = MakeHand(CurrentHand.Bet, new List<GameObject> { CurrentHand.Cards[1] });
        CurrentHand = SplitedHand1;
        OnSplit?.Invoke();
        ReadyForTurn();
    }  

    public void Double()
    {
        Bet(CurrentHand.Bet);
        CurrentHand.Bet *= 2;
        Hit();
        if (CurrentHand.Points < 21)
        {
            Stand();
        }
    }

    public override void Hit()
    {
        base.Hit();
        if (CurrentHand.Points < 21)
        {
            ReadyForTurn();
        }
    }

    public override void Stand()
    {
        OnStand?.Invoke();
        if (Splited)
        {
            if (CurrentHand == SplitedHand1)
            {
                CurrentHand = SplitedHand2;
                ReadyForTurn();
            }
            else
            {
                OnTurnsEnd?.Invoke();
            }
        }
        else
        {
            OnTurnsEnd?.Invoke();
        }
        CurrentHand.OnBust -= Stand;
        CurrentHand.OnBlackJack -= Stand;
    }

    public void TakeChips(int chips)
    {
        Chips += chips;
    }

    public void ReadyForTurn()
    {
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.5f);
            OnTurn?.Invoke();
        }
        StartCoroutine(Wait());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Person
{
    [field: SerializeField] public int Chips { get; private set; }

    public UnityEvent<int> OnBet;
    public UnityEvent OnTurn;
    public UnityEvent OnSplit;

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
        OnSplit?.Invoke();
        Bet(CurrentHand.Bet);
        SplitedHand1 = MakeHand(CardPlace,CurrentHand.Bet, new List<GameObject> { CurrentHand.Cards[0] });
        Vector3 posForSecondHand = CardPlace;
        posForSecondHand.z += 1.5f;
        SplitedHand2 = MakeHand(posForSecondHand, CurrentHand.Bet, new List<GameObject> { CurrentHand.Cards[1] });
        SplitedHand1.Cards[0].transform.position = CardPlace;
        SplitedHand2.Cards[0].transform.position = posForSecondHand;
        CurrentHand = SplitedHand1;
        Splited = true;
        ReadyForTurn();
    }  

    public void Double()
    {
        Bet(CurrentHand.Bet);
        CurrentHand.DoubleBet();
        base.Hit();
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
        StopAllCoroutines();
        CurrentHand.OnBust -= Stand;
        CurrentHand.OnBlackJack -= Stand;
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

    public override void ResetHand()
    {
        if (!Splited)
        {
            base.ResetHand();
        }
        else
        {
            ClearTable(SplitedHand1);
            ClearTable(SplitedHand2);
            Splited = false;
        }
    }
}

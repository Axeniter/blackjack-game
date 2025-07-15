using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Person : MonoBehaviour
{
    public class Hand
    {
        public List<GameObject> Cards { get; private set; }
        public int Bet { get; private set; }
        public int Points { get; private set; }
        public bool Busted { get; private set; }

        public event Action OnBust;
        public event Action<int> OnPointsChange;
        public event Action OnBlackJack;

        public Hand(int bet = 0, List<GameObject> cards = null)
        {
            Busted = false;
            Cards = cards ?? new();
            Bet = bet;
            CountPoints();
        }

        public void Take(GameObject card)
        {
            Cards.Add(card);
            CountPoints();
        }

        public void DoubleBet()
        {
            Bet *= 2;
        }

        private void CountPoints()
        {
            Points = 0;
            List<Card> aces = new();
            foreach (GameObject obj in Cards)
            {
                Card card = obj.GetComponent<Card>();
                if (card.IsAce)
                {
                    aces.Add(card);
                }
                else
                {
                    Points += card.Value;
                }
            }
            if (aces.Count > 0)
            {
                foreach (Card card in aces)
                {
                    Points += 1;
                }
                if (Points <= 11)
                {
                    Points += 10;
                }
            }
            OnPointsChange?.Invoke(Points);
            if (Points > 21)
            {
                Busted = true;
                OnBust?.Invoke();
            }
            if (Points == 21)
            {
                OnBlackJack?.Invoke();
            }
        }
    }

    public Hand CurrentHand { get; set; }

    [field: SerializeField] public Vector3 CardPlace { get; private set; }

    public UnityEvent<Person> OnHit;
    public UnityEvent OnTurnsEnd;

    public virtual void Hit()
    {
        OnHit?.Invoke(this);
    }

    public virtual void Stand()
    {
        CurrentHand.OnBust -= Stand;
        CurrentHand.OnBlackJack -= Stand;
        StopAllCoroutines();
        OnTurnsEnd?.Invoke();
    }

    public Hand MakeHand(int bet = 0, List<GameObject> cards = null)
    {
        Hand hand = new(bet, cards);
        hand.OnBust += Stand;
        hand.OnBlackJack += Stand;
        return hand;
    }

    public void ClearTable(Hand hand)
    {
        foreach (GameObject card in hand.Cards)
        {
            Destroy(card);
        }
        hand.Cards.Clear();
    }

    public virtual void ResetHand()
    {
        ClearTable(CurrentHand);
        CurrentHand = null;
    }
}

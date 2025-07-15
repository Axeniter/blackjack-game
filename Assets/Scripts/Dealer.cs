using System.Collections;
using UnityEngine;

public class Dealer : Person
{
    public void DealerTake()
    {
        IEnumerator DealerMove()
        {
            while (CurrentHand.Points < 17)
            {
                Hit();
                yield return new WaitForSeconds(0.5f);
            }
            if (CurrentHand.Points < 21)
            {
                OnTurnsEnd?.Invoke();
            }
        }
        StartCoroutine(DealerMove());
    }
}

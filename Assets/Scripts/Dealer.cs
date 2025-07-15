using System.Collections;
using UnityEngine;

public class Dealer : Person
{
    [SerializeField] private float delay;
    public void DealerTake()
    {
        IEnumerator DealerMove()
        {
            while (CurrentHand.Points < 17)
            {
                yield return new WaitForSeconds(delay);
                Hit();
            }
            if (CurrentHand.Points < 21)
            {
                OnTurnsEnd?.Invoke();
            }
        }
        StartCoroutine(DealerMove());
    }
}

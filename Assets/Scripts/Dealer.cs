using System.Collections;
using UnityEngine;

public class Dealer : Person
{
    public IEnumerator DealerMove()
    {
        while (points < 17)
        {
            CardTake();
            yield return new WaitForSeconds(0.5f);
        }
        if (points > 21)
        {
            OnBust?.Invoke(this);
        }
    }
}

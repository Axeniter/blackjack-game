using UnityEngine;
using TMPro;

public class PointsShower : MonoBehaviour
{
    [SerializeField] private TMP_Text pointText;
    [SerializeField] private Person person;
    [SerializeField] private int handNum;
    private Person.Hand hand;
    private void Awake()
    {
        if (person is Player)
        {
            switch (handNum)
            {
                case 1:
                    hand = (person as Player).SplitedHand1;
                    break;
                case 2:
                    hand = (person as Player).SplitedHand2;
                    break;
                default:
                    hand = person.CurrentHand;
                    break;
            }
        }
        else
        {
            hand = person.CurrentHand;
        }
    }

    public void ChangePoints()
    {
        pointText.text = hand.Points.ToString();
    }

    private void OnEnable()
    {
        hand.OnPointsChange += ChangePoints;
    }

    private void OnDisable()
    {
        hand.OnPointsChange -= ChangePoints;
    }

    
}

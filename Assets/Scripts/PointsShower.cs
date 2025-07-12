using UnityEngine;
using TMPro;

public class PointsShower : MonoBehaviour
{
    [SerializeField] private TMP_Text pointText;

    public void ChangePoints(int points)
    {
        pointText.text = points.ToString();
    }
}

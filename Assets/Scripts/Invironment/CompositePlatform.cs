using UnityEngine;

public class CompositePlatform : MonoBehaviour
{
    [SerializeField] private GameObject _segmentUp;
    [SerializeField] private GameObject _segmentRight;
    [SerializeField] private GameObject _segmentDown;
    [SerializeField] private GameObject _segmentLeft;

    public void SetActiveSegmentUp(bool isActive)
    {
        _segmentUp.SetActive(isActive);
    }
    public void SetActiveSegmentDown(bool isActive)
    {
        _segmentDown.SetActive(isActive);
    }
    public void SetActiveSegmentLeft(bool isActive)
    {
        _segmentLeft.SetActive(isActive);
    }

    public void SetActiveSegmentRight(bool isActive)
    {
        _segmentRight.SetActive(isActive);
    }
}

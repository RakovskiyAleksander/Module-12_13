using UnityEngine;
public class CountdownTimer
{
    private float _setTime;
    private float _timeLeft;
    private bool _isTicking;

    public CountdownTimer(float initialTime)
    {
        _setTime = initialTime;
        _timeLeft = _setTime;
        TimeIsOver = false;
        _isTicking = true;
    }

    public bool TimeIsOver { get; private set; }
    public bool IsTicking => _isTicking;
    public int TimeLeft => (int)_timeLeft;

    public void Update()
    {
        if (TimeIsOver == false && _isTicking == true)
        {
            _timeLeft -= Time.deltaTime;
        }

        if (_timeLeft < 0)
        {
            _timeLeft = 0f;
            TimeIsOver = true;
        }
    }
    public void Pause()
    {
        _isTicking = false;
    }

    public void Resume()
    {
        _isTicking = true;
    }

    public void Reset()
    {
        _timeLeft = _setTime;
    }
}


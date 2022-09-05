using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextNumberHelper : MonoBehaviour
{
    [Header("Set runtime")]
    public float Time = 0.3f;
    [Header("Set frame per second")]
    public int Fps = 20;

    public float runTime = 0.2f;

    private Text _numberTxt;

    private double _curNum;
    private double _diffPerSec;
    private long _targetNum;
    private Coroutine _corIeIncreaseNumber;
    private Coroutine _corIeDecreaseNumber;

    void Awake()
    {
        _numberTxt = gameObject.GetComponent<Text>();
    }

    public void SetColor(Color color)
    {
        _numberTxt.color = color;
    }

    public void IncreaseTo(long toNum)
    {
        if(toNum == 0)
        {
            SetTo(toNum);
            return;
        }
        if (gameObject == null) return;
        if (!gameObject.activeInHierarchy)
        {
            SetTo(toNum);
            return;
        }

        _diffPerSec = (toNum - _curNum) / (Fps * Time);
        _targetNum = toNum;

        if (toNum > _curNum)
        {
            if (_curNum < toNum * 5f / 10) SetTo(toNum * 7 / 10);
            if (_corIeIncreaseNumber != null) StopCoroutine(_corIeIncreaseNumber);
            _corIeIncreaseNumber = StartCoroutine(IeIncreaseNumber());
        }
        else if (toNum < _curNum)
        {
            if (_curNum > toNum * 2) SetTo(toNum * 12 / 10);
            if (_corIeDecreaseNumber != null) StopCoroutine(_corIeDecreaseNumber);
            _corIeDecreaseNumber = StartCoroutine(IeDecreaseNumber());
        }
    }

    public void FastIncreaseTo(long toNum)
    {
        if (gameObject == null) return;
        if (!gameObject.activeInHierarchy)
        {
            SetTo(toNum);
            return;
        }

        Time = runTime;

        // from
        _curNum += (toNum - _curNum) * 9 / 10;
        _diffPerSec = (toNum - _curNum) / (Time * Fps);
        _targetNum = toNum;

        if (toNum > _curNum)
        {
            if (_corIeIncreaseNumber != null) StopCoroutine(_corIeIncreaseNumber);
            _corIeIncreaseNumber = StartCoroutine(IeIncreaseNumber());
        }
        else if (toNum < _curNum)
        {
            if (_corIeDecreaseNumber != null) StopCoroutine(_corIeDecreaseNumber);
            _corIeDecreaseNumber = StartCoroutine(IeDecreaseNumber());
        }
    }

    public void IncreaseFromZeroTo(long toNum, float time = 0.2f)
    {
        Time = time;
        _curNum = 0;
        _diffPerSec = (toNum - _curNum) / (Fps * Time);
        _targetNum = toNum;

        if (_corIeIncreaseNumber != null) StopCoroutine(_corIeIncreaseNumber);
        _corIeIncreaseNumber = StartCoroutine(IeIncreaseNumber());
    }

    public void SetTo(long toNum)
    {
        _curNum = toNum;
        if (_numberTxt == null) _numberTxt = GetComponent<Text>();
        _numberTxt.text = Utils.NumberGroup((long)_curNum);
    }

    public void SetToNull()
    {
        if (_corIeIncreaseNumber != null) StopCoroutine(_corIeIncreaseNumber);
        if (_corIeDecreaseNumber != null) StopCoroutine(_corIeDecreaseNumber);
        if (_numberTxt)
        {
            _curNum = 0;
            _numberTxt.text = "";
        }
    }

    public long GetValue()
    {
        return (long)_curNum;
    }

    private IEnumerator IeIncreaseNumber()
    {
        if (_numberTxt == null) _numberTxt = GetComponent<Text>();
        if (_numberTxt == null)
        {
            Debug.LogError(gameObject.name + " hasn't text component");
            yield break;
        }

        _curNum += _diffPerSec;
        if (_curNum >= _targetNum) yield break;
        while (_curNum < _targetNum)
        {
            _numberTxt.text = Utils.NumberGroup((long)_curNum);
            yield return new WaitForSeconds(1f / Fps);
            _curNum += _diffPerSec;
        }

        _curNum = _targetNum;
        _numberTxt.text = Utils.NumberGroup((long)_curNum);
    }

    private IEnumerator IeDecreaseNumber()
    {
        if (_numberTxt == null) _numberTxt = GetComponent<Text>();
        if (_numberTxt == null)
        {
            Debug.LogError(gameObject.name + " hasn't text component");
            yield break;
        }

        _curNum += _diffPerSec;
        if (_curNum <= _targetNum) yield break;
        while (_curNum > _targetNum)
        {
            _numberTxt.text = Utils.NumberGroup((long)_curNum);
            yield return new WaitForSeconds(1f / Fps);
            _curNum += _diffPerSec;
        }

        _curNum = _targetNum;
        _numberTxt.text = Utils.NumberGroup((long)_curNum);
    }
}

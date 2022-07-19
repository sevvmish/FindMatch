using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Panel : MonoBehaviour
{
    [SerializeField] private RectTransform hidePanel;
    [SerializeField] private TextMeshProUGUI currentNumberText;
    [SerializeField] private Button currentButton;
    public int number;
    public bool isBusy;
    public bool isHidden;
    public bool isOpened;

    private readonly float hideSpeed = 0.2f;
    private readonly float showTime = 1f;


    private void OnEnable()
    {
        currentButton.onClick.AddListener(()=> 
        {
            if (!isBusy && GameControl.CurrentCount.Count<2)
            {
                StartCoroutine(ShowAndHidePanel());
            }
        });

        HidePanel();
    }

    public void SetNumber(int _number)
    {
        number = _number;
        currentNumberText.text = number.ToString();
    }



    public void HidePanel()
    {
        hidePanel.DORotate(new Vector3(0, 0, 0), hideSpeed);
        hidePanel.DOScale(new Vector3(1, 1, 1), hideSpeed);
        isHidden = true;
    }

    public IEnumerator ShowAndHidePanel()
    {
        isBusy = true;
        isHidden = false;
        GameControl.CurrentCount.Add(this);
        hidePanel.DORotate(new Vector3(0, 90, 0), hideSpeed);
        hidePanel.DOScale(new Vector3(1, 0, 1), hideSpeed);
        yield return new WaitForSeconds(hideSpeed);

        yield return new WaitForSeconds(showTime);

        hidePanel.DORotate(new Vector3(0, 0, 0), hideSpeed);
        hidePanel.DOScale(new Vector3(1, 1, 1), hideSpeed);
        yield return new WaitForSeconds(hideSpeed);
        isBusy = false;
        isHidden = true;
        GameControl.CurrentCount.Remove(this);
    }

    public void ShowAndStay()
    {
        isBusy = true;
        isOpened = true;
        isHidden = false;
        hidePanel.DORotate(new Vector3(0, 90, 0), hideSpeed);
        hidePanel.DOScale(new Vector3(1, 0, 1), hideSpeed);
    }


}

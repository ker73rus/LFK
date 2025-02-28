using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UltimatumGame : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI Text;
    [SerializeField]
    Slider slider;
    [SerializeField]
    int type = 1;
    [SerializeField]
    GameObject ResultPanel;
    [SerializeField]
    TextMeshProUGUI ResultText;
    [SerializeField]
    UltimatumLevels level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        slider.value = 0;
        Text.text = "0%";
        ResultPanel.SetActive(false);
    }
    public void OnSliderChange()
    {
        slider.value = (float)Math.Round(slider.value*100, 0)/100;
        Text.text = slider.value * 100 + "%";
    }
    public void Offer()
    {
        switch (type)
        {
            case 1:
                Accept();
                break;
            case 2:
                if(slider.value >= 0.7f)
                    Deny();
                else Accept(); 
                break;
            case 3:
                int t = UnityEngine.Random.Range(0, 2);
                if(t == 1)
                {
                    Accept();
                }
                else Deny();
                break;
        }
    }
    public void Accept()
    {
        StartCoroutine(Result("Ваш аппонент принял предложение, вы получаете " + slider.value *100 + "% от золота"));
    }
    public void Deny()
    {
        StartCoroutine(Result("Ваш аппонент отклонил предложение, вы получаете 100% золота"));

    }
    IEnumerator Result(string text)
    {
        yield return new WaitForSeconds(1);
        ResultPanel.SetActive(true);
        ResultText.text = text;
        yield return new WaitForSeconds(2);
        level.toLevelsPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

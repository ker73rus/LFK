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
    [SerializeField]
    int Turn = 1;
    [SerializeField]
    float player = 0;
    [SerializeField]
    float ai = 0;
    [SerializeField]
    GameObject offerButton;
    [SerializeField]
    GameObject offerPanel;
    [SerializeField]
    TextMeshProUGUI offerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        Turn = 1;
        player = 0;
        ai = 0;
        slider.value = 0;
        Text.text = "0%";
        ResultPanel.SetActive(false);
        offerButton.SetActive(true);
        offerPanel.SetActive(false);
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
                Accept();
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
    public void AiOffer(float per)
    {
        slider.value = (100-per) / 100;
        slider.value = (float)Math.Round(slider.value * 100, 0) / 100;
        offerText.text = "Оппонент предлагает вам " + (100 - per) + "% золота \n Примите предложение?";
    }
    public void PlayerAccept()
    {
        StartCoroutine(Result("Вы приняли предложение оппонента, и получаете " + (1 - slider.value < 0.01f ? 0.01f : 1 - slider.value) * 100 + "% от золота"));
        offerPanel.SetActive(false);
    }
    public void PlayerDeny()
    {
        slider.value = 1;
        StartCoroutine(Result("Вы отклонили предложение оппонента, и получаете 0% от золота"));
        offerPanel.SetActive(false);
    }
    public void Accept()
    {
        StartCoroutine(Result("Ваш аппонент принял предложение, вы получаете " + slider.value *100 + "% от золота"));
    }
    public void Deny()
    {
        slider.value = 1;
        StartCoroutine(Result("Ваш аппонент отклонил предложение, вы получаете 100% золота"));

    }
    IEnumerator Result(string text)
    {
        player += slider.value;
        ai +=  1 - slider.value < 0.01f ? 0.01f : 1 - slider.value;
        if(Turn < 2)
        {
            yield return new WaitForSeconds(1);
            ResultPanel.SetActive(true);
            ResultText.text = text;
            yield return new WaitForSeconds(2);
            ResultPanel.SetActive(false);
            Turn++;
            if(Turn == 2)
            {
                offerButton.SetActive(false);
                switch (type) {
                    case 1:
                        AiOffer(50);
                        break;
                    case 2:
                        if (ai < 0.3f)
                        {
                            AiOffer( UnityEngine.Random.Range((int)(player*100), 100));
                        }
                        else AiOffer(Math.Abs(1-player < 0.01f ? 0.01f : 1-player)*100);
                        break;
                    case 3:
                        AiOffer(UnityEngine.Random.Range(0, 100));
                        break;
                }
                offerPanel.SetActive(true);

            }

        }
        else
        {
            string analysis = "\n";
            switch (type)
            {
                case 1:
                    analysis += "Ваш оппонент был рационален, он принимал предложение, даже если бы он оказался в меньшинстве. А вам предложил разделить пополам. Для победы стоило предложить хотя бы на 1 процент более чем половина, что вы и сделали, предложив " + (player - 0.5f) * 100;
                    break;
                case 2:
                    analysis += "Ваш оппонент был эмоциональным, он принимал предложение, даже если бы он оказался в меньшинстве.А вам предлагал такое, чтобы точно не проиграть, а в идеале победить, ведь вы его обидели. Победить тут сложно, оппонент всегда будет стараться вывести на ничью ";
                    break;
                case 3:
                    analysis += "Ваш оппонент случайно принимал решения, для победы нужно было лишь чуточку удачи";
                    break;


            }
            if (player > ai)
            {
                ResultText.text = "Вы выйграли набрав " + player * 100;
            }
            else if(player < ai)
            {
                ResultText.text = "Вы проиграли набрав " + player * 100 + " против " + ai * 100 + " у оппонента";
            }
            else
                ResultText.text = "Ничья! Вы набрали " + player * 100 + " против " + ai * 100 + " у оппонента";
            ResultText.text += analysis;
            level.Win();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using Assets.Scripts;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class StoryGame : MonoBehaviour
{
    int Difficulty = 1;
    int page = -1;
    [SerializeField]
    List<GameObject> pages;
    [SerializeField]
    GameObject StoryPanel;
    [SerializeField]
    GameObject AskPanel;
    [SerializeField]
    GameObject LevelPanel;
    [SerializeField]
    GameObject LosePanel;
    [SerializeField]
    AudioSource WinAudio;
    [SerializeField]
    AudioSource back;
    string path ="/saves.txt";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Begin()
    {
        path = Application.persistentDataPath + "/saves.txt";
        GetFromFile();
    }
    void GetFromFile()
    {
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Close();
            }
            page = -1;
            Difficulty = 0;
            LevelPanel.SetActive(true);
            StoryPanel.SetActive(false);
        }
        else
        {
            if(page >=0)
                pages[page].SetActive(false);
            LevelPanel.SetActive (false);
            StoryPanel.SetActive(false);
            AskPanel.SetActive(true);
        }
    }

    void SaveIntoFile()
    {
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(Difficulty + "," + page);
                sw.Close();
            }
        }
        else
        {
            File.Delete(path);
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(Difficulty + "," + page);
                sw.Close();
            }
        }
    }

    public void NextPage()
    {
        SaveIntoFile();
        if (page + 1 < pages.Count)
        { page++; Visual(); }
        else
            Win();
        
    }
    public void Win()
    {
        pages.Last().SetActive(false);
        StoryPanel.SetActive(false);
    }
    public void Visual()
    {
        pages[page].SetActive(true);
        if (page != 0)
        {
            pages[page - 1].SetActive(false);
        }
        if (pages[page].CompareTag("Stones"))
        {
            MainGame game = pages[page].GetComponent<MainGame>();
            game.story = true;
            game.lose = false;
            game.win = false;
            if (page -2 > 0 && pages[page - 2].CompareTag("Stones"))
            {
                game.LoadLevel(Difficulty == 3 ? 5 : Difficulty);
            }
            else game.LoadLevel(0);
            StartCoroutine(waitStoneWin());
        }
        else
        if (!pages[page].CompareTag("Story"))
        {
            StoryPanel.SetActive(false);
            Levels l = pages[page].GetComponent<Levels>();
            l.story = true;
            l.lose = false;
            l.win = false;
            switch (Difficulty)
            {
                case 1:
                    l.loadLevel1();
                    break;
                case 2:
                    l.loadLevel2();

                    break;
                case 3:
                    l.loadLevel3();
                    break;
            }
            StartCoroutine(waitWin());
        }
        else
        {
            StoryPanel.SetActive(true);
        }
        
    }
    IEnumerator waitWin()
    {
        while (!pages[page].GetComponent<Levels>().win && !pages[page].GetComponent<Levels>().lose)
        {
            print("∆‰Û");
            yield return new WaitForSeconds(1f);
        }
        if (pages[page].GetComponent<Levels>().win)
        {
            WinAudio.Play();
            NextPage();
        }
        else if (pages[page].GetComponent<Levels>().lose)
        {
            File.Delete(path);
            LosePanel.SetActive(true);
        }
    }
    IEnumerator waitStoneWin()
    {
        while (!pages[page].GetComponent<MainGame>().win && !pages[page].GetComponent<MainGame>().lose)
        {
            print("∆‰Û");
            yield return new WaitForSeconds(.05f);
        }
        if (pages[page].GetComponent<MainGame>().win)
        {
            WinAudio.Play();
            NextPage();

        }
        else if (pages[page].GetComponent<MainGame>().lose)
        {
            File.Delete(path);
            LosePanel.SetActive(true);
        }
    }

    public void SetEasyDifficulty()
    {
        Difficulty = 1;
        page = -1;
        NextPage();
    }
    public void SetMediumDifficulty()
    {
        Difficulty = 2;
        page = -1;
        NextPage();
    }
    public void SetHardDifficulty()
    {
        Difficulty = 3;
        page = -1;
        NextPage();
    }
    public void Continue()
    {
        AskPanel.SetActive(false);
        LevelPanel.SetActive(false);
        using (StreamReader sr = File.OpenText(path))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                var c = s.Split(",");
                int? d = int.Parse(c[0]);
                int? p = int.Parse(c[1]);
                if (d != null && p != null)
                {
                    Difficulty = d.Value;
                    page = p.Value;
                }
            }
        }
        NextPage();
    }
    public void NewGame()
    {
        if(page >=0)
            pages[page].SetActive(false);
        Difficulty = 0;
        page = -1;
        AskPanel.SetActive (false);
        LosePanel.SetActive (false);
        StoryPanel.SetActive (false);
        LevelPanel.SetActive (true);
    }
    public void RepeatPage()
    {
        LosePanel.SetActive(false);
        Visual();
    }
}

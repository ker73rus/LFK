using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeGame : MonoBehaviour
{
    [SerializeField]
    List<Num> nums;
    [SerializeField]
    List<int> solution;
    [SerializeField]
    CodeLevels levels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        solution.Clear();
        foreach (var item in nums)
        {
            int t = Random.Range(0, 9);
            while (solution.Contains(t)) {
                t = Random.Range(0, 9);
            }
            if(!solution.Contains(t))
                solution.Add(t);
            item.curNum = 0;
            item.Text.text = item.curNum.ToString();
            item.SetState(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TryOpen()
    {
        foreach (var item in nums) {
            int t = solution.IndexOf(item.curNum);
            if (t == -1)
            {
                item.SetState(1);
            }
            else
            {
                if (t == nums.IndexOf(item)) { 
                    item.SetState(3);
                }
                else
                {
                    item.SetState(2);
                }
            }
        
        }
        bool flag = true;
        foreach (var item in nums)
        {
            if (nums.IndexOf(item) != solution.IndexOf(item.curNum)) { flag = false; break; }
        }
        if (flag)
        {
            levels.Win();
        }
    }
}

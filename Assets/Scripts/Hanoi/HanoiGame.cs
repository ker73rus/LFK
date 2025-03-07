using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class Comparer : IComparer<GameObject>
{
    public int Compare(GameObject x, GameObject y)
    {
        int xid = x.GetComponent<Ring>().id;
        int yid = y.GetComponent<Ring>().id;
        return xid > yid ? -1 : 1;
    }
}
public class HanoiGame : MonoBehaviour
{
    [SerializeField]
    GameObject leftTower;
    [SerializeField]
    GameObject rightTower;
    [SerializeField]
    GameObject centerTower;
    [SerializeField]
    HanoiLevels levels;

    [SerializeField]
    GameObject curRing;
    public int count = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public  void Start()
    {
        List<GameObject> rings = new();
        Tower lt = leftTower.GetComponent<Tower>();
        Tower ct = centerTower.GetComponent<Tower>();
        Tower rt = rightTower.GetComponent<Tower>();
        foreach (var r in lt.rings)
        {
            rings.Add(r);
        }
        foreach (var r in ct.rings)
        {
            rings.Add(r);
        }
        foreach (var r in rt.rings)
        {
            rings.Add(r);
        }
        rings.Sort(new Comparer());
        lt.rings.Clear();
        rt.rings.Clear();
        ct.rings.Clear();
        foreach (var r in rings)
        {
            lt.rings.Add(r);
            r.GetComponent<Ring>().curTower = 1;
        }
        lt.Start();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectRing(GameObject ring)
    {
        if (curRing == null)
        {
            Tower tower;
            switch (ring.GetComponent<Ring>().curTower)
            {
                case 1:
                    tower = leftTower.GetComponent<Tower>();
                    break;
                case 2:
                    tower = centerTower.GetComponent<Tower>();
                    break;
                case 3:
                    tower = rightTower.GetComponent<Tower>();
                    break;
                default:
                    tower = new Tower();
                    break;
            }
            if (tower.TryCatch(ring))
            {
                curRing = ring;
                curRing.GetComponent<RectTransform>().localPosition += new Vector3(0, 350 - curRing.GetComponent<RectTransform>().localPosition.y);
            }
        }
        
    }
    public void PutOnTower(int id)
    {
        if (curRing != null)
        {
            Tower tower;
            Ring ring = curRing.GetComponent<Ring>();
            int prevTower = ring.curTower;
            switch (id)
            {
                case 1:
                    tower = leftTower.GetComponent<Tower>();
                    break;
                case 2:
                    tower = centerTower.GetComponent<Tower>();
                    break;
                case 3:
                    tower = rightTower.GetComponent<Tower>();
                    break;
                default:
                    tower = new Tower();
                    break;
            }
            if (!tower.Put(curRing))
            {
                PutOnTower(ring.curTower);
            }
            else
            {
                if(ring.curTower != prevTower)
                 DropRing(curRing, prevTower);
            }
            curRing = null;
            if (rightTower.GetComponent<Tower>().rings.Count >= count) {
                levels.Win();
            }

        }
    }
    void DropRing(GameObject ring, int prevTower)
    {
        Tower tower;
        switch (prevTower)
        {
            case 1:
                tower = leftTower.GetComponent<Tower>();
                break;
            case 2:
                tower = centerTower.GetComponent<Tower>();
                break;
            case 3:
                tower = rightTower.GetComponent<Tower>();
                break;
            default :
                tower = new Tower();
                break;
        }
        tower.Drop(ring);

    }

}


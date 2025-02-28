using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public List<GameObject> rings;
    [SerializeField]
    int id = 0;
    [SerializeField]
    HanoiGame game;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => game.PutOnTower(id));
        foreach (var r in rings)
        {
            Put(r);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Put(GameObject ring)
    {
        if (rings.Contains(ring))
        {
            ring.GetComponent<RectTransform>().localPosition = new Vector3(-800 + (id * 400),-300 + (60 * rings.IndexOf(ring)));
            return true;
        }
        else
        {
            if(rings.Count == 0 || rings.Last().GetComponent<Ring>().id >= ring.GetComponent<Ring>().id)
            {
                rings.Add(ring);
                ring.GetComponent<RectTransform>().localPosition = new Vector3(-800 + (id * 400), -360 + (60 * rings.Count()));
                ring.GetComponent<Ring>().curTower = id;
                return true;

            }
            else return false;
        }
    }
    public void Drop(GameObject ring) {
        if (rings.Contains(ring))
        {
            rings.Remove(ring);
        }
    
    }
    public bool TryCatch(GameObject ring) { 
        if(ring == rings.Last())
        {
            return true;
        }
        else return false ;
    
    }
}

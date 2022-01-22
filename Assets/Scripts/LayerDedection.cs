using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDedection : MonoBehaviour
{
    [SerializeField] private Transform treeHolder;

    [SerializeField] private List<Transform> trees = new List<Transform>();
    [SerializeField] private int layerMultiplier;
    private void Start()
    {
        FindAllTrees();
        ArrangeTrees();
    }
    private void FindAllTrees()
    {
        for (int i = 0; i < treeHolder.childCount; i++)
        {
            trees.Add(treeHolder.GetChild(i));
        }
    }
    private void ArrangeTrees()
    {
        for (int i = 0; i < trees.Count; i++)
        {
            int listNumber = trees.Count - 1;
            for (int j = 0; j < trees.Count; j++)
            {
                if(i != j)
                {
                    if(trees[i].transform.position.y > trees[j].transform.position.y)
                    {
                        listNumber--;
                    }
                }
            }
            if(i != listNumber)
            {
                Transform temp = trees[listNumber];
                trees[listNumber] = trees[i];
                trees[i] = temp;
            }
        }
        for (int i = 0; i < trees.Count; i++)
        {
            trees[i].GetComponent<SpriteRenderer>().sortingOrder = (i + 1) * layerMultiplier;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGround : Grid
{
    [SerializeField] GridGround currentGrid;
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    public void ChangeGridProperty()
    {
        if (currentGrid != null)
        {
            Destroy(currentGrid.gameObject);
        }
        currentGrid = Instantiate((GridGround)managerSO.ListGridMap[0], TF);
    }
}

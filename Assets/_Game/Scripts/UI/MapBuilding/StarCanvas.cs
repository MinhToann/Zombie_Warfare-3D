using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCanvas : MonoBehaviour
{
    //[SerializeField] List<StarBuilding> listStar = new List<StarBuilding>();
    [SerializeField] List<Image> listStarImg = new List<Image>();
    public List<Image> ListStar => listStarImg;
    [SerializeField] List<StarBuilding> listStarBuilding = new List<StarBuilding>();
    public List<StarBuilding> ListStarBuilding => listStarBuilding;
    public void ChangeColorStar(int index, int r, int g, int b, int a)
    {
        Color colorStar = new Color(r, g, b, a);
        listStarImg[index].color = colorStar;
    }    
    
}

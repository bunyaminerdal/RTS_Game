using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;


public class RingMenuController : MonoBehaviour
{
    public Transform location;
    public Ring Data;

    [SerializeField]
    private GameObject RingCakePiecePrefab;

    private float GapWidthDegree = 3f;
    private GameObject[] Pieces;
    private float stepLength;

    public void CreatedMenu()
    {
        stepLength = 360f / Data.Elements.Length;
        var iconDist = Vector3.Distance(RingCakePiecePrefab.GetComponent<RingCakePiece>().Icon.transform.position, RingCakePiecePrefab.GetComponent<RingCakePiece>().CakePiece.transform.position);
        //Position it
        Pieces = new GameObject[Data.Elements.Length];
        for (int i = 0; i < Data.Elements.Length; i++)
        {

            Pieces[i] = Instantiate(RingCakePiecePrefab, transform);
            //set root element
            Pieces[i].transform.localPosition = Vector3.zero;
            Pieces[i].transform.localRotation = Quaternion.identity;
            RingCakePiece ringCakePiece = Pieces[i].GetComponent<RingCakePiece>();
            //set cake piece
            ringCakePiece.CakePiece.fillAmount = 1f / Data.Elements.Length - GapWidthDegree / 360f;
            ringCakePiece.CakePiece.transform.localPosition = Vector3.zero;
            ringCakePiece.CakePiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2f + GapWidthDegree / 2f + i * stepLength);
            ringCakePiece.CakePiece.color = new Color(1f, 1f, 1f, 0.3f);
            //set icon
            ringCakePiece.Icon.transform.localPosition = ringCakePiece.CakePiece.transform.localPosition + Quaternion.AngleAxis((i-1) * stepLength, Vector3.forward) * Vector3.up * iconDist;
            ringCakePiece.Icon.sprite = Data.Elements[i].Icon;
            var button = Pieces[i].GetComponentInChildren<Button>();
            button.onClick.AddListener(Data.Elements[i].ElementAction);
            if (Data.Elements[i].Name == "Empty")
            {
                ringCakePiece.Icon.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
}

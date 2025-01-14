using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class ChessUIManager : MonoBehaviour
    {
       [SerializeField] private GameObject GameOverScreen;
       [SerializeField] private TMP_Text resultText;

       public void HideUI()
       {
           GameOverScreen.SetActive(false);
       }

       public void OnGameFinished(string winner)
       {
           GameOverScreen.SetActive(true);
           resultText.text = string.Format("{0} won ",winner);
       }  
    }
}

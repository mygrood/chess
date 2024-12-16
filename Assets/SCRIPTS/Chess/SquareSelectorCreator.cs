using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class SquareSelectorCreator :MonoBehaviour
    {
        [SerializeField] private Material freeSquareMaterial;
        [SerializeField] private Material opponentSquareMaterial;
        [SerializeField] private GameObject selectorPrefab;
        private List<GameObject> selectors = new List<GameObject>();

        public void ShowSelection(Dictionary<Vector3, bool> squareData)
        {
            ClearSelection();
            foreach (var data in squareData)
            {
                GameObject selector = Instantiate(selectorPrefab,data.Key,Quaternion.identity);
                selectors.Add(selector);
                foreach (var setter in selector.GetComponentsInChildren<MaterialSetter>())
                {
                    setter.SetSingleMaterial(data.Value? freeSquareMaterial: opponentSquareMaterial);
                }
            }
        }
        

        public void ClearSelection()
        {
            foreach (var selector in selectors)
            {
                Destroy(selector);
            }
            selectors.Clear();
        }
    }
}
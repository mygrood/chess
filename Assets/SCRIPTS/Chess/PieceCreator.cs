using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class PieceCreator:MonoBehaviour
    {
        [SerializeField] private GameObject[] piecePrefabs;
        [SerializeField] private Material blackMaterial;
        [SerializeField] private Material whiteMaterial;
        
        private Dictionary<string, GameObject> nameToPieceDictionary = new Dictionary<string, GameObject>();

        private void Awake()
        {
            foreach (var piece in piecePrefabs)
            {
                nameToPieceDictionary.Add(piece.GetComponent<Piece>().GetType().ToString(),piece);
                //Debug.Log(piece.GetComponent<Piece>().GetType().ToString());
            }
        }

        public GameObject CreatePiece(Type type)
        {
            Debug.Log(type.ToString());
            GameObject prefab = nameToPieceDictionary[type.ToString()];
            if (prefab)
            {
                GameObject newPiece = Instantiate(prefab);
                return newPiece;
            }
            Debug.LogError($"Prefab for type {type.ToString()} not found in the dictionary.");
            return null;
        }

        public Material GetTeamMaterial(TeamColor team)
        {
            return team == TeamColor.White ? whiteMaterial : blackMaterial;
        }
        
    }
}
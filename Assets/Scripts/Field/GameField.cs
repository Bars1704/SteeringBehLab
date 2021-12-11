using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public Vector2 Size => Vector3.one * RectSize;

    [SerializeField] private float RectSize;

    [SerializeField] private AnimalsFabric _animalsFabric;

    private void FixedUpdate()
    {
        var animalsToKill = new List<AnimalBase>();
        
        foreach (var animal in _animalsFabric.Animals)
        {
            var magnitude = animal.transform.position - transform.position;
            magnitude.x = Mathf.Abs(magnitude.x);
            magnitude.y = Mathf.Abs(magnitude.y);

            if (magnitude.x > Size.x / 2 || magnitude.y > Size.y / 2)
                animalsToKill.Add(animal);
        }
        
        animalsToKill.ForEach(x=>x.Kill());
    }
}
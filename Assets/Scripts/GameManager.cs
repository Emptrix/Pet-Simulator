using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface meshSurface;
    [SerializeField]
    private int maxFoodAllowedInLevel = 10;
    [SerializeField]
    private float foodSpawnFrequency = 3;
    [SerializeField]
    private Mob mob;
    [SerializeField]
    private Food foodPrefab = null;
    public UserInterface userInterface;

    private void Start()
    {
        StartCoroutine(FoodSpawns());
    }

    Food freshFood;
    Vector3 foodSpawnLocation = Vector3.zero;
    private IEnumerator FoodSpawns()
    {
        while (true)
        {
            if (Food.activeFoods.Count < maxFoodAllowedInLevel)
            {
                if (Food.inactiveFoods.Count > 0)
                {
                    freshFood = Food.inactiveFoods[0];
                }
                else
                {
                    freshFood = Instantiate(foodPrefab.gameObject, transform).GetComponent<Food>();
                }

                freshFood.Initialize(userInterface);
                mob.GetRandomPointOnNavMeshSurface(meshSurface.center, 50, out foodSpawnLocation);
                freshFood.transform.position = foodSpawnLocation + Vector3.up * 2F;
                freshFood.gameObject.SetActive(true);
                Debug.Log(Food.inactiveFoods.Count + " inactive foods, " + Food.activeFoods.Count + " active foods.");
            }

            yield return new WaitForSeconds(foodSpawnFrequency);

        }
    }
}

[System.Serializable]
public class UserInterface
{
    public TextMeshProUGUI foodTextMeshProUGUI = null;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public static List<Food> inactiveFoods = new List<Food>();
    public static List<Food> activeFoods = new List<Food>();
    public static int totalFoodConsumed = 0;

    [SerializeField]
    private int health = 3;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private UserInterface userInterface;
    private int defaultHealth = 0;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        defaultHealth = health;    
    }

    private void OnEnable()
    {
        health = defaultHealth;
        inactiveFoods.Remove(this);
        activeFoods.Add(this);
    }

    public void Initialize(UserInterface userInterface)
    {
        this.userInterface = userInterface;
    }

    public void Consume()
    {
        health--;
        rb.AddForce(Vector3.one, ForceMode.Impulse);
        if (health <= 0)
            Expire();
    }

    private void Expire()
    {
        rb.position = Vector3.up * 100;
        totalFoodConsumed++;
        userInterface.foodTextMeshProUGUI.text = "Food Consumed: " + totalFoodConsumed;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        inactiveFoods.Add(this);
        activeFoods.Remove(this);
    }
}

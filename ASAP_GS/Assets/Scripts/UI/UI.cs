using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    List<GameObject> hitPointsIcons = new List<GameObject>();

    [SerializeField] Transform hitPointSprite;
    public PlayerController player;
    int currentHealth;

    private void Awake()
    {
        
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        UIIntit();

        HealthPoints.onHPChange += UIUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UIIntit()
    {
        currentHealth = player.GetCurrentHealth();
        Vector2 hpOrigin = (Vector2)GameObject.Find("HitPointOrigin").transform.localPosition;

        for (int i = 0; i < currentHealth; i++)
        {
            Transform hitPoint = Instantiate(hitPointSprite, transform);
            hitPoint.localPosition = hpOrigin + new Vector2(35 * i, 0);

            hitPointsIcons.Add(hitPoint.gameObject);
        }



    }

    void UIUpdate()
    {
        foreach (GameObject item in hitPointsIcons)
        {
            Destroy(item);
        }

        hitPointsIcons.Clear();

        UIIntit();
    }
}

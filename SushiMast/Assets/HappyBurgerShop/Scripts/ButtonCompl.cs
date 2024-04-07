using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCompl : MonoBehaviour
{
    private GameObject customers;
    private GameObject ingredients;
    public bool isOrderReady = false;
    private bool isButtonPressed = false;

    public Slider progressSlider;

    private float buttonPressTime = 0f;
    private const float REQUIRED_PRESS_TIME = 5f; // 5 секунд

    private void OnEnable()
    {
        MainGameController.OnCustomerSpawned += HandleCustomerSpawned;
    }

    private void OnDisable()
    {
        MainGameController.OnCustomerSpawned -= HandleCustomerSpawned;
    }

    private void HandleCustomerSpawned(GameObject customer)
    {
        // Сохраняем ссылку на созданный объект customer
        customers = customer;
    }
    // Start is called before the first frame update
    void Update()
    {
        // Проверяем условия для обновления прогресса
        if (isButtonPressed
            && customers != null
            && customers.GetComponent<CustomerController>() != null 
            && customers.GetComponent<CustomerController>().isOnSeat
            && MainGameController.deliveryQueueItems == CustomerController.orderIngredientsIDs.Length)
        {
            buttonPressTime += Time.deltaTime;
            progressSlider.value = buttonPressTime / REQUIRED_PRESS_TIME;

            if (buttonPressTime >= REQUIRED_PRESS_TIME)
            {
                SetOrderReady();
                isButtonPressed = false;
                buttonPressTime = 0f;
                progressSlider.value = 0f;
            }
            else if(PlayerPrefs.GetInt("shopItem-1") == 1)
            {
                SetOrderReady();
            }
        }
    }
    public void ButtonPressed()
    {
        isButtonPressed = true;
        buttonPressTime = 0f;
    }

    public void ButtonReleased()
    {
        isButtonPressed = false;

    }

    public void SetOrderReady()
    {
        ingredients = GameObject.FindGameObjectWithTag("ingredient");

        if (customers != null && customers.GetComponent<CustomerController>() != null && customers.GetComponent<CustomerController>().isOnSeat)
        {
            isOrderReady = true;

            //check if order is finished and completed
            if (MainGameController.deliveryQueueItems == CustomerController.orderIngredientsIDs.Length)
            {
                //order is complete!
                print("Order is done!");
                ingredients.GetComponent<IngredientsController>().playSfx(ingredients.GetComponent<IngredientsController>().successfulDelivery);
                customers.GetComponent<CustomerController>().settle();
            }
        }
    }
}

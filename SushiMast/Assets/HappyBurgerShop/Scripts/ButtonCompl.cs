using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCompl : MonoBehaviour
{
    private GameObject customers;
    private GameObject ingredients;
    public bool isOrderReady = false;

    private float buttonPressTime = 0f;
    private const float REQUIRED_PRESS_TIME = 5f; // 5 секунд


    // Start is called before the first frame update
    void Update()
    {
        
    }
    public void SetOrderReady()
    {
        ingredients = GameObject.FindGameObjectWithTag("ingredient");
        customers = GameObject.FindGameObjectWithTag("customer");
        Debug.Log("ff");
        if (customers.GetComponent<CustomerController>().isOnSeat)
        {
            isOrderReady = true;
            Debug.Log("ss");
            //check if order is finished and completed
            if (MainGameController.deliveryQueueItems == CustomerController.orderIngredientsIDs.Length)
            {
                //order is complete!
                print("Order is done!");
                ////wait
                //yield return new WaitForSeconds(0.2f);
                //playSfx(successfulDelivery);
                //tell customer to settle and leave
                Debug.Log("dd");
                customers.GetComponent<CustomerController>().settle();
            }
        }
    }
}

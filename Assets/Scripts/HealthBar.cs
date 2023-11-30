using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; 
    public GameObject boss;

    void Start()
    {
        slider.maxValue = boss.GetComponent<Boss2>().hp; // Set the maximum value of the slider to the max health
        slider.value = boss.GetComponent<Boss2>().hp; // Set the current value of the slider to the max health at the start
    }
    void Update()
    {
        slider.value = boss.GetComponent<Boss2>().hp; ; // Set the current value of the slider to the current health
    }
  
    

    
     
    
}
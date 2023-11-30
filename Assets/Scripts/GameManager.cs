using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;

   
    public static GameManager Instance
    {
        get
        {
          
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

               
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                    DontDestroyOnLoad(singletonObject); 
                }
            }
            return _instance;
        }
    }

    // variables 

    public bool hasBlueGem;
    public bool hasRedGem;

    private void Awake()
    {
      
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    
}

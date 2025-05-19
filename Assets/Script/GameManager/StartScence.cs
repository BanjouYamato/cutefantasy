using UnityEngine;

public class StartScence : MonoBehaviour
{   
    public static StartScence instance;
    private void Awake()
    {   
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
    }
}

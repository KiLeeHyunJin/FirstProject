using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    PlayerController playerController;
    public float Hp 
    { 
        get 
        {
            if (playerController == null)
                return 0;
            else
                return playerController.Hp / (float)playerController.MaxHp; 
        } 
    }
    public float Mp
    {
        get
        {
            if (playerController == null)
                return 0;
            else
                return playerController.Mp / (float)playerController.MaxMp;
        }
    }
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

}

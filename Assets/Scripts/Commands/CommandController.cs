using UnityEngine;

public class CommandController : MonoBehaviour
{
    public static CommandController Instance { get; private set; }

    ICommand activeCommand;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ExecuteCommand(ICommand command)
    {
        if (activeCommand != null)
        {
            return; 
        }
        activeCommand = command;
        command.Execute();
    }

    private void Update()
    {
        if(activeCommand != null)
        {
            if (activeCommand.Completed)
            {
                activeCommand = null;
            }
            else
            {
                activeCommand.Update();
            }
        }
    }
}

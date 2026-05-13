namespace GameProject.Commands;

public class NullCommand : ICommand
{
    public void Execute()
    {
    }
    
    public void Undo()
    {
    }
    
    public string GetDescription() => "Нет действия";
}
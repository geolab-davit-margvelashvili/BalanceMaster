namespace BalanceMaster.Domain.Abstractions;

public interface ICommand
{
    public void Validate();
}
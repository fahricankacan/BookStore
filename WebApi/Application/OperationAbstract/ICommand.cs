namespace WebApi.Application.OperationAbstract
{
    public interface ICommand
    {
        void Handle();
        ICommand Model { get; set; }

    }
}
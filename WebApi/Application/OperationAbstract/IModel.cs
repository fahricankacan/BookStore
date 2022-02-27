namespace WebApi.Application.OperationAbstract
{
    public interface ICommand<T>
    {

        T Model { get; set; }

    }
}
namespace WebApi.Application.OperationAbstract
{
    public interface IQuery<T>
    {
        T Handle();
    }
}
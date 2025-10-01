namespace CleanArchiSoitec.Infrastructure.Repositories
{
    public interface IFinder<T>
    {

        public IReadOnlyCollection<T> GetAll();

        public T Get(int id);
    }
}
namespace Practical19.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IStudentRepository StudentRepository { get; }
    Task<int> SaveChangesAsync();
}

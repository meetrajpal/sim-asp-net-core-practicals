namespace Practical18.Domain.Models.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IStudentRepository StudentRepository { get; }
    Task<int> SaveChangesAsync();
}

namespace Practical20.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepo { get; }
    IRoleRepository RoleRepo { get; }
    IUserRoleRepository UserRoleRepo { get; }
    IStudentRepository StudentRepo { get; }
    Task<int> SaveChangesAsync();
}

namespace UnitOfWork;

public interface IUserService
{
	User AddUser(string name, int age);
}

public class UserService : IUserService
{
	private readonly IUnitOfWork _unitOfWork;

	public UserService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public User AddUser(string name, int age)
	{
		var user = new User(Guid.NewGuid(), name, age);

		_unitOfWork.AddOrUpdate(user);

		return user;
	}
}










public record User(Guid Id, string Name, int Age);






public interface IUserRepository
{
	void Save(User user);
}

public class UserRepository : IUserRepository
{
	public void Save(User user)
	{
		Console.WriteLine("Saved user with name {0} and age {1}. Id = {2}", user.Name, user.Age, user.Id);
	}
}
namespace UnitOfWork;

public interface IUserGroupService
{
	UserGroup AddUserGroup(IEnumerable<User> users, string name);
}

public class UserGroupService : IUserGroupService
{
	private readonly IUnitOfWork _unitOfWork;

	public UserGroupService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public UserGroup AddUserGroup(IEnumerable<User> users, string name)
	{
		var userGroup = new UserGroup(Guid.NewGuid(), users, name);

		_unitOfWork.AddOrUpdate(userGroup);

		return userGroup;
	}
}


public record UserGroup(Guid Id, IEnumerable<User> Users, string Name);




public interface IUserGroupRepository
{
	void Save(UserGroup userGroup);
}

public class UserGroupRepository : IUserGroupRepository
{
	public void Save(UserGroup userGroup)
	{
		Console.WriteLine("Saved user group with name {0} and Id = {1}. User Ids: {2}", userGroup.Name, userGroup.Id,
			string.Concat(userGroup.Users.Select(u => u.Id).ToString()));
	}
}
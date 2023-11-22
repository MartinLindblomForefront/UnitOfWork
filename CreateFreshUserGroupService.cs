namespace UnitOfWork;

public record FreshUserData(string Name, int Age);

public class CreateFreshUserGroupService
{
	private readonly IUserService _userService;
	private readonly IUserGroupService _userGroupService;
	private readonly IUnitOfWork _unitOfWork;

	public CreateFreshUserGroupService(IUserService userService, IUserGroupService userGroupService, IUnitOfWork unitOfWork)
	{
		_userService = userService;
		_userGroupService = userGroupService;
		_unitOfWork = unitOfWork;
	}

	public UserGroup? CreateFreshUserGroup(IEnumerable<FreshUserData> userData, string groupName)
	{
		var users = new List<User>();
		UserGroup? userGroup = null;

		try
		{
			foreach (var ud in userData)
			{
				var user = _userService.AddUser(ud.Name, ud.Age);
				users.Add(user);
			}

			userGroup = _userGroupService.AddUserGroup(users, groupName);


			_unitOfWork.SaveChanges();
		}
		catch (Exception ex)
		{
			_unitOfWork.DiscardChanges();
		}

		return userGroup;
	}
}
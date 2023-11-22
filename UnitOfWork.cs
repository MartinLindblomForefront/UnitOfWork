namespace UnitOfWork;

public interface IUnitOfWork
{
	void AddOrUpdate(object obj);
	void Remove(object obj);
	void SaveChanges();
	void DiscardChanges();
}

internal class UnitOfWork : IUnitOfWork
{
	private readonly IList<object> _trackedObjects = new List<object>();

	private readonly IUserRepository _userRepository;
	private readonly IUserGroupRepository _userGroupRepository;

	public UnitOfWork(IUserRepository userRepository, IUserGroupRepository userGroupRepository)
	{
		_userRepository = userRepository;
		_userGroupRepository = userGroupRepository;
	}

	public void AddOrUpdate(object obj)
	{
		_trackedObjects.Add(obj);
	}

	public void Remove(object obj)
	{
		_trackedObjects.Remove(obj);
	}

	public void SaveChanges()
	{
		foreach (var trackedObject in _trackedObjects)
		{
			switch (trackedObject)
			{
				case User user:
					_userRepository.Save(user);
					break;
				case UserGroup userGroup:
					_userGroupRepository.Save(userGroup);
					break;
			}
		}
	}

	public void DiscardChanges()
	{
		_trackedObjects.Clear();
	}
}
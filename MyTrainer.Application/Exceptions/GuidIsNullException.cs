using System;
namespace MyTrainer.Application.Exceptions;

public class GuidIsNullException: Exception
{
	public GuidIsNullException(string message) : base(message)
	{ }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrainer.Persistence.Exceptions;

public class DeleteException: Exception
{
    public DeleteException(string message)
        : base(message)
    { }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Model
{
    internal class Users : IUsers
    {
        public Guid UserId => Guid.NewGuid();
    }
}

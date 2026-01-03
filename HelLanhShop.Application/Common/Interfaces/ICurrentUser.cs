using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        int? UserId { get; }
    }
}

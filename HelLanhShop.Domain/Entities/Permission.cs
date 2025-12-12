using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;   //
        public string Code { get; set; } = null!; // PRODUCT.CREATE, ORDER.DELETE
        public string Description { get; set; } = null!;

        public ICollection<RolePermission> RolePermissions { get; set; } = null!;
        public ICollection<UserPermission> UserPermissions { get; set; } = null!;
    }
}

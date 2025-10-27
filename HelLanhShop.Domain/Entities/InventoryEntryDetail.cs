using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public class InventoryEntryDetail : BaseEntity
    {
        public int Id { get; set; }

        // FK tới phiếu nhập
        public int InventoryEntryId { get; set; }
        public InventoryEntry? InventoryEntry { get; set; }

        // FK tới sản phẩm
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public decimal Quantity { get; set; }    // Số lượng nhập
        public decimal Cost { get; set; }        // Giá nhập 1 đơn vị
        public decimal Total => Quantity * Cost; // Tổng tiền nhập
    }

}

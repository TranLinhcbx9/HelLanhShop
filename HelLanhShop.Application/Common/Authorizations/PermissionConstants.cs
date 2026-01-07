using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Authorizations
{
    public static class PermissionConstants
    {
        public static class Product
        {
            public const string View = "PRODUCT.VIEW";
            public const string Create = "PRODUCT.CREATE";
            public const string Update = "PRODUCT.UPDATE";
            public const string Delete = "PRODUCT.DELETE";
        }
        //public static IReadOnlyList<string> All()
        //{
        //    return new[]
        //    {
        //        Product.ProductView,
        //        Product.ProductCreate,
        //        Product.ProductUpdate,
        //        Product.ProductDelete
        //    };
        //}
        public static IReadOnlyList<string> All =>
            typeof(PermissionConstants)
                .GetNestedTypes()
                .SelectMany(t =>
                    t.GetFields(BindingFlags.Public | BindingFlags.Static)
                     .Where(f => f.FieldType == typeof(string))
                     .Select(f => f.GetValue(null)!.ToString()!)
                )
                .Distinct()
                .ToList();

    }
}

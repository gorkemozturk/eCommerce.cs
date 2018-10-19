using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSeceltListProductType<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("ProductTypeName"),
                       Value = item.GetPropertyValue("ProductTypeID"),
                       Selected = item.GetPropertyValue("ProductTypeID").Equals(selectedValue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSeceltListTag<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("TagName"),
                       Value = item.GetPropertyValue("TagID"),
                       Selected = item.GetPropertyValue("TagID").Equals(selectedValue.ToString())
                   };
        }
    }
}

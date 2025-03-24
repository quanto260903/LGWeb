using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Domain.Models
{
    public class MenuCreateViewModel
    {
        public Menu Menu { get; set; }
        public int? ParentId { get; set; }
        public MenuPositionType Type { get; set; }
        [ValidateNever]
        public IEnumerable<MenuItem> MenuItems { get; set; }

        public class MenuItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Level { get; set; } // Cấp độ của mục trong cây
             public List<MenuItem> Children { get; set; } = new List<MenuItem>();
            public string Prefix => new string('-', Level * 2); 
        }

       
    }
    public enum MenuPositionType
    {
        Header = 0,
        Footer = 1
    }
}

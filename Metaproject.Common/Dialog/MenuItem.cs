using System.Collections.Generic;
using System.Linq;

namespace Metaproject.Common
{
    public class MenuItem
    {

        public static List<MenuItem> SubItems(params MenuItem[] items)
        {
            return items.ToList();
        }

        public static MenuItem Item(string caption, List<MenuItem> items = null)
        {
            return new MenuItem
            {
                Caption = caption,
                Children = items
            };
        }


        public static MenuItem Item(string caption, string commandId)
        {
            return new MenuItem
            {
                 Caption = caption,
                 CommandId = commandId,
            };
        }
  

        
        public string Caption { get; set; }
        public string CommandId { get; set; }
        public bool IsEnabled { get; set; } = true;

        public List<MenuItem> Children = new List<MenuItem>();
        public MenuItemType ItemType { get; set; } = MenuItemType.Commmand;
    }
}

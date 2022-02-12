using System.Collections.Generic;
using System.Windows.Controls;

namespace Software_Design_Examples.View_Model
{
    public class VendingList
    {
        public List<VendingItem> VendingItems { get; set; } = new List<VendingItem>();
    }
    public class VendingItem
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public Image ButtonImage { get; set; }
        public string ImageSource { get; set; }
        public bool OutOfStock { get; set; }
    }
}

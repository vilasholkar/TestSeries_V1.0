using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Master
{
    public class NotificationViewModel
    {
        public int NotificationID { get; set; }
        public int ReciverID { get; set; }
        public string DeviceToken { get; set; }
        public string NotificationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string RedirectToURL { get; set; }
        public bool IsRead { get; set; }
    }
}

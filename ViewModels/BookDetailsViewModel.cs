using System;
using System.Collections.Generic;

namespace BookMania.ViewModels
{
    public class BookDetailsViewModel : BookItemViewModel
    {
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
    }
}
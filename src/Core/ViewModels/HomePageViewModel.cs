using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace Core.ViewModels
{
    public class HomePageViewModel
    {
        public StaticPagedList<PublicationViewModel> Publications { get; set; }
        public IReadOnlyCollection<PublicationViewModel> TopPublications { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Siders { get; set; }
        public IEnumerable<Articulo> ListArticulos { get; set; }


    }
}

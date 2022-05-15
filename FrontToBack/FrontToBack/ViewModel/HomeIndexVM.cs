using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FrontToBack.Models;

namespace FrontToBack.ViewModel
{
    public class HomeIndexVM
    {
        public List<Slider> Sliders { get; set; }
        public About About { get; set; }
        public Parallax1 Parallax1 { get;set; }
    }
}
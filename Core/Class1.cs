using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IFigure
    {

    }

    class Circle : IFigure
    {
        public double Radius { get; set; }
    }

    class Rectangle : IFigure
    {
        public double Height { get; set; }
        public double Width { get; set; }
    }

    class Ellipse : IFigure
    {
        public double XRadius { get; set; }
        public double YRadius { get; set; }
    }


    static class Parser
    {
        public static string Parse(IFigure figure)
        {
            switch(figure)
            {
                case Circle c:
                    break;

            }
        }




    }
}

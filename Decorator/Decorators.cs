using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labs_RPM.Bridge;

namespace labs_RPM.Decorator
{
    public class BorderDecorator : ShapeDecorator
    {
        public BorderDecorator(Shape shape) : base(shape) { }
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine(" + Adding Border");
        }
    }

    public class ShadowDecorator : ShapeDecorator
    {
        public ShadowDecorator(Shape shape) : base(shape) { }
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine(" + Adding Shadow");
        }
    }
}

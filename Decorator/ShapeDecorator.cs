using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labs_RPM.Bridge;

namespace labs_RPM.Decorator
{
    public abstract class ShapeDecorator : Shape
    {
        protected Shape _decoratedShape;
        public ShapeDecorator(Shape shape) : base(null) => _decoratedShape = shape;
        public override void Draw() => _decoratedShape.Draw();
    }
}

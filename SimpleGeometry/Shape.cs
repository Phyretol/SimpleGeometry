using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGeometry {
    public interface Shape {
        Rect BoundingBox();
        void Translate(Vector2 offset);
        void Rotate(Vector2 pivot, float rotation);
        bool Overlaps(Shape shape);
        bool Contains(Vector2 point);

        Shape Clone();
    }
}

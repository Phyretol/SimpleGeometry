using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGeometry.Shapes {
    public class Circle : Shape {
        private Vector2 center;
        private float radius;

        public Circle(Vector2 center, float radius) {
            this.center = center;
            this.radius = radius;
        }

        public Vector2 Center { get => center; }
        public float Radius { get => radius; }

        public Rect BoundingBox() {
            return new Rect(center.x - radius, center.y + radius, radius * 2, radius * 2);
        }

        public bool Contains(Vector2 point) {
            return (point.Distance(center) < radius);
        }

        public void Rotate(Vector2 pivot, float rotation) { }

        public void Translate(Vector2 offset) {
            center += offset;
        }

        public bool Overlaps(Shape shape) {
            if (shape is Segment)
                return Geometry.SegmentOverlapsCircle((Segment)shape, this);
            else if (shape is Circle)
                return Geometry.CircleOverlapsCircle(this, (Circle)shape);
            else if (shape is Polygon)
                return Geometry.PolygonOverlapsCircle((Polygon)shape, this);
            else
                return false;
        }

        public Shape Clone() {
            return new Circle(center, radius);
        }
    }
}

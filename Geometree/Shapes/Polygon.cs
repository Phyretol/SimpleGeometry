using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGeometry.Shapes {
    public class Polygon : Shape {
        protected Segment[] segments;
        protected Rect boundingBox;

        public Polygon(Segment[] segments) {
            this.segments = segments;
            UpdateBoundingBox();
        }

        public Polygon(Vector2 center, float width, float height) {
            segments = new Segment[4];
            float left = center.x - width / 2;
            float right = center.x + width / 2;
            float top = center.y + height / 2;
            float bottom = center.y - height / 2;
            segments[0] = new Segment(new Vector2(left, top), new Vector2(right, top));
            segments[1] = new Segment(new Vector2(right, top), new Vector2(right, bottom));
            segments[2] = new Segment(new Vector2(right, bottom), new Vector2(left, bottom));
            segments[3] = new Segment(new Vector2(left, bottom), new Vector2(left, top));
            boundingBox = new Rect(left, top, width, height);
        }

        public Segment[] Segments { get => segments; }

        public Rect Rect { get => boundingBox; }

        public int N { get => segments.Count(); }

        public Rect BoundingBox() {
            return boundingBox;
        }

        private void UpdateBoundingBox() {
            float bottom, top, left, right;
            top = right = float.NegativeInfinity;
            bottom = left = float.PositiveInfinity;

            for (int i = 0; i < segments.Length; i++) {
                Vector2 vertex = segments[i].Start;
                if (vertex.x < left)
                    left = vertex.x;
                else if (vertex.x > right)
                    right = vertex.x;
                if (vertex.y < bottom)
                    bottom = vertex.y;
                else if (vertex.y > top)
                    top = vertex.y;
            }

            float width = right - left;
            float height = top - bottom;
            boundingBox = new Rect(left, top, width, height);
        }

        public bool Contains(Vector2 point) {
            Segment ray = Segment.RayForward(point);
            int n = 0;
            foreach (Segment s in segments) {
                if (Geometry.SegmentIntersectsSegment(s, ray))
                    n++;
            }
            return (n % 2 == 0);
        }

        public bool Overlaps(Shape shape) {
            if (shape is Segment)
                return Geometry.SegmentOverlapsPolygon((Segment)shape, this);
            else if (shape is Circle)
                return Geometry.PolygonOverlapsCircle(this, (Circle)shape);
            else if (shape is Polygon)
                return Geometry.PolygonOverlapsPolygon(this, (Polygon)shape);
            else
                return false;
        }

        public void Rotate(Vector2 pivot, float rotation) {
            segments[0].End.Rotate(pivot, rotation);
            for (int i = 1; i < N; i++) {
                segments[i].Start = segments[i - 1].End;
                segments[i].End.Rotate(pivot, rotation);
            }
            segments[0].Start = segments[N - 1].End;
            UpdateBoundingBox();
        }

        public void Translate(Vector2 offset) {
            foreach (Segment s in segments)
                s.Translate(offset);
            boundingBox.Offset(offset.x, offset.y);
        }

        public Shape Clone() {
            return new Polygon(segments);
        }
    }
}

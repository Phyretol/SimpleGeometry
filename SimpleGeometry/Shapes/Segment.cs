using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGeometry.Shapes {
    public sealed class Segment : Shape {
        private Vector2 start;
        private Vector2 end;
        private Line line;
        private bool lineDirty;

        public Segment(Vector2 start, Vector2 end) {
            this.start = start;
            this.end = end;
            lineDirty = true;
        }

        public static Segment RayForward(Vector2 start) {
            return new Segment(start, new Vector2(start.x, float.PositiveInfinity));
        }

        public Vector2 Start {
            get => start;
            set {
                start = value;
                lineDirty = true;
            }
        }
        public Vector2 End {
            get => end;
            set {
                end = value;
                lineDirty = true;
            }
        }

        public bool IsVertical { get => start.x == end.x; }

        public Line GetLine() {
            if (IsVertical)
                return default(Line);
            if (lineDirty) {
                float slope = (end.y - start.y) / (end.x - start.x);
                float yIntercept = start.y - (slope * start.x);
                line = new Line(slope, yIntercept);
                lineDirty = false;
            }
            return line;
        }

        public void Translate(Vector2 offset) {
            start += offset;
            end += offset;
        }

        public Rect BoundingBox() {
            float left = Math.Min(start.x, end.x);
            float right = Math.Max(start.x, end.x);
            float bottom = Math.Min(start.y, end.y);
            float top = Math.Max(start.y, end.y);
            float width = right - left;
            float height = top - bottom;
            return new Rect(left, top, width, height);
        }

        public void Rotate(Vector2 pivot, float rotation) {
            throw new NotImplementedException();
        }

        public bool Overlaps(Shape shape) {
            if (shape is Segment)
                return Geometry.SegmentIntersectsSegment(this, (Segment)shape);
            else if (shape is Circle)
                return Geometry.SegmentOverlapsCircle(this, (Circle)shape);
            else if (shape is Polygon)
                return Geometry.SegmentOverlapsPolygon(this, (Polygon)shape);
            else
                return false;
        }

        public bool Contains(Vector2 point) {
            return false;
        }

        public Shape Clone() {
            return new Segment(start, end);
        }

        public override string ToString() {
            return start.ToString() + " " + end.ToString();
        }
    }
}

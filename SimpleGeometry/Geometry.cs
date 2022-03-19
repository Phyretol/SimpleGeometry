using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleGeometry.Shapes;

namespace SimpleGeometry {
    public class Geometry {
        public static bool CircleOverlapsCircle(Circle c1, Circle c2) {
            return (c1.Center.Distance(c2.Center) < c1.Radius + c2.Radius);
        }

        public static bool PolygonOverlapsPolygon(Polygon p1, Polygon p2) {
            foreach (Segment s1 in p1.Segments) {
                foreach (Segment s2 in p2.Segments) {
                    if (SegmentIntersectsSegment(s1, s2))
                        return true;
                }
            }

            return (p1.Contains(p2.Segments[0].Start) || p2.Contains(p1.Segments[0].Start));
        }

        public static bool PolygonOverlapsCircle(Polygon polygon, Circle circle) {
            foreach (Segment s in polygon.Segments) {
                if (circle.Contains(s.Start))
                    return true;
            }

            foreach (Segment s in polygon.Segments) {
                if (SegmentIntersectsCircle(s, circle))
                    return true;
            }

            return polygon.Contains(circle.Center);
        }

        public static bool SegmentOverlapsPolygon(Segment segment, Polygon polygon) {
            foreach (Segment s in polygon.Segments) {
                if (SegmentIntersectsSegment(s, segment))
                    return true;
            }

            return (polygon.Contains(segment.Start)
                || polygon.Contains(segment.End));
        }

        public static bool SegmentOverlapsCircle(Segment segment, Circle circle) {
            if (circle.Contains(segment.Start) || circle.Contains(segment.End))
                return true;

            return SegmentIntersectsCircle(segment, circle);
        }

        public static bool SegmentIntersectsCircle(Segment segment, Circle circle) {
            Vector2 c = circle.Center;
            float r = circle.Radius;

            if (segment.IsVertical) {
                float x = segment.Start.x;
                float y1 = (float)Math.Sqrt(-c.x * c.x + 2 * c.x * x + r * r - x * x) + c.y;
                float y2 = c.y - (float)Math.Sqrt(-c.x * c.x + 2 * c.x * x + r * r - x * x);
                float yMin = Math.Min(segment.Start.y, segment.End.y);
                float yMax = Math.Max(segment.Start.y, segment.End.y);

                return (y1 >= yMin && y1 <= yMax) || (y2 >= yMin && y2 <= yMax);
            }

            Line line = segment.GetLine();
            float a = -2 * c.x;
            float b = -2 * c.y;
            float m = line.slope;
            float q = line.yIntercept;
            float A = 1 + m * m;
            float B = 2 * m * q + m * b + a;
            float C = c.x * c.x + c.y * c.y + q * q + b * q - r * r;
            float D = B * B - 4 * A * C;
            if (D < 0)
                return false;

            float x1 = (-B - (float)Math.Sqrt(D)) / (2 * A);
            float x2 = (-B + (float)Math.Sqrt(D)) / (2 * A);
            Vector2 p1 = new Vector2(x1, line.Eval(x1));
            Vector2 p2 = new Vector2(x2, line.Eval(x2));
            return (segment.BoundingBox().Contains(p1.x, p1.y)
                || segment.BoundingBox().Contains(p2.x, p2.y));
        }

        public static bool SegmentIntersectsSegment(Segment s1, Segment s2) {
            Vector2 p;
            if (s1.IsVertical && s2.IsVertical)
                return s1.Start.x == s2.Start.x;
            if (s1.IsVertical || s2.IsVertical) {
                Segment vertical = null, other = null;
                if (s1.IsVertical) {
                    vertical = s1;
                    other = s2;
                } else {
                    vertical = s2;
                    other = s1;
                }
                Line line = other.GetLine();
                p = new Vector2(vertical.Start.x, line.Eval(vertical.Start.x));
            } else 
                s1.GetLine().Intersection(s2.GetLine(), out p);
            return (s1.BoundingBox().Contains(p.x, p.y)
                && s2.BoundingBox().Contains(p.x, p.y));
        }

        public static Vector2 ClosestPointOnSegment(Vector2 point, Segment segment) {
            float x = point.x;
            float y = point.y;
            float x1 = segment.Start.x;
            float y1 = segment.Start.y;
            float x2 = segment.End.x;
            float y2 = segment.End.y;

            var A = x - x1;
            var B = y - y1;
            var C = x2 - x1;
            var D = y2 - y1;

            var dot = A * C + B * D;
            var len_sq = C * C + D * D;

            float param = -1;
            if (len_sq != 0) //in case of 0 length line
                param = dot / len_sq;

            float xx, yy;

            if (param < 0) {
                xx = x1;
                yy = y1;
            } else if (param > 1) {
                xx = x2;
                yy = y2;
            } else {
                xx = x1 + param * C;
                yy = y1 + param * D;
            }

            return new Vector2(xx, yy);
        }
    }
}

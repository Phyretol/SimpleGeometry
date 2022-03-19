using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGeometry {
    public struct Line {
        public const float bigSlope = 1000f;
        public float slope;
        public float yIntercept;

        public Line(float slope, float yIntercept) {
            this.slope = slope;
            this.yIntercept = yIntercept;
        }

        public void Intersection(Line other, out Vector2 point) {
            float x = (other.yIntercept - yIntercept) / (slope - other.slope);
            float y = Eval(x);
            point = new Vector2(x, y);
        }

        public float Eval(float x) {
            return slope * x + yIntercept;
        }
    }
}

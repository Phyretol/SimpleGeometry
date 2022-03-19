using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGeometry {
    public struct Rect {
        public float left;
        public float right;
        public float top;
        public float bottom;

        public Rect(float x, float y, float width, float height) {
            left = x;
            top = y;
            right = x + width;
            bottom = y - height;
        }

        public float Width { get => right - left; }
        public float Height { get => top - bottom; }

        public bool Contains(Rect rectangle) {
            return rectangle.left >= left && rectangle.right <= right && rectangle.top <= top && rectangle.bottom >= bottom;
        }

        public bool Contains(float x, float y) {
            return x >= left && x <= right && y >= bottom && y <= top;
        }

        public bool Intersects(Rect rectangle) {
            return rectangle.right >= left && rectangle.left <= right && rectangle.bottom <= top && rectangle.top >= bottom;
        }

        internal void Offset(float x, float y) {
            left += x;
            right += x;
            top += y;
            bottom += y;
        }
    }
}

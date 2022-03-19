using SimpleGeometry.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGeometry {
    public static class Program {
        public static void Main(string[] args) {
            Segment s1 = new Segment(new Vector2(-1, -1), new Vector2(1, 1));
            Segment s2 = new Segment(new Vector2(-1, 1), new Vector2(1, -1));
            Console.WriteLine("s1 x s2 : " + s1.Overlaps(s2));  //true
            Segment s3 = new Segment(new Vector2(2, -2), new Vector2(2, 2));
            Console.WriteLine("s1 x s3 : " + s1.Overlaps(s3));  //false
            Circle c = new Circle(new Vector2(2, 0), 1);
            Console.WriteLine("s4 x c : " + s3.Overlaps(c));    //true
            Console.WriteLine("s1 x c : " + s1.Overlaps(c));    //false
            Console.WriteLine(c.Contains(c.Center));    //true
            Console.WriteLine(c.Contains(s1.Start));    //false
            Console.Read();
        }
    }
}

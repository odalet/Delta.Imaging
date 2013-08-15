using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestExif.Drawing.Imaging
{
    internal abstract class BaseExifRational<T> where T : struct 
    {
        protected T n;
        protected T d;

        public BaseExifRational(T t1, T t2)
        {
            n = t1;
            d = t2;

            Simplify(ref n, ref d);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", n, d);
        }

        public double ToDouble() { return ToDouble(2); }

        public abstract double ToDouble(int digits);

        public abstract void Simplify(ref T a, ref T b);        
    }
    
    internal class ExifRational : BaseExifRational<uint>
    {
        public ExifRational(uint n, uint d) : base(n, d) { }

        public override double ToDouble(int digits)
        {
            if (d == 0)
            {
                if (n == 0) return double.NaN;
                else return double.PositiveInfinity;
            }
            else return Math.Round((double)n / (double)d, digits);
        }

        public override string ToString()
        {
            if (d == 1u) return n.ToString();
            return base.ToString();
        }

        public override void Simplify(ref uint a, ref uint b)
        {
            if (a == 0 || b == 0) return;

            var gcd = Euclid(a, b);
            a /= gcd;
            b /= gcd;
        }

        private uint Euclid(uint a, uint b)
        {
            return b == 0 ? a : Euclid(b, a % b);
        }
    }

    internal class ExifSRational : BaseExifRational<int>
    {
        public ExifSRational(int n, int d) : base(n, d) { }

        public override double ToDouble(int digits)
        {
            if (d == 0)
            {
                if (n == 0) return double.NaN;
                else return double.PositiveInfinity;
            }
            else return Math.Round((double)n / (double)d, digits);
        }

        public override string ToString()
        {
            if (d == 1) return n.ToString();
            return base.ToString();
        }

        public override void Simplify(ref int a, ref int b)
        {
            if (b < 0)
            {
                a = -a;
                b = -b;
            }

            if (a < 0)
            {
                int ma = -a;
                DoSimplify(ref ma, ref b);
                a = -ma;
            }
            else DoSimplify(ref a, ref b);
        }

        private void DoSimplify(ref int a, ref int b)
        {
            if (a == 0 || b == 0) return;

            var gcd = Euclid(a, b);
            a /= gcd;
            b /= gcd;
        }

        private int Euclid(int a, int b)
        {
            return b == 0 ? a : Euclid(b, a % b);
        }
    }
}

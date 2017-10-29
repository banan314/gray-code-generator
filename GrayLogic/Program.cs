using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace GrayLogic
{

    class Program
    {
        class Pause
        {
            public Pause()
            {
                Console.Write("Press any key to continue . . .");
                Console.ReadKey(true);
            }
        }

        static void Main(string[] args)
        {
            GrayCode grayCode = new GrayCode();

            int n = 2;
            grayCode.n = n;
            int numberOfWords = GrayCode.powerOf2(n);

            grayCode.generateCode(grayCode.n);

            TestWord();

            var res = grayCode.result;
            for(int i = 0; i < GrayCode.powerOf2(n); i++)
            {
                Console.WriteLine(res[i]);
            }
            new Pause();
        }

        static void TestWord()
        {
            GrayWord gw = new GrayWord();
            gw.zero(4);
            Debug.Assert(gw.isEmpty());
            gw.negate(1);
            Debug.Assert(gw.ToString().Equals( "0, 1, 0, 0"));
            Debug.Assert(gw.findFirst1() == 1);
            Debug.Assert(!gw.isEmpty());
            Debug.Assert(gw.getHowMany() == 1);
            gw.negate(2);
            Debug.Assert(gw.getHowMany()==2);
        }
    }
}

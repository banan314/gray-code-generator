using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrayLogic
{
    class GrayWord
    {
        private int n;
        private bool[] word;
        public GrayWord()
        {

        }

        public void zero(int n)
        {
            this.n = n;
            word = new bool[n];
            for (int i = 0; i < n; i++)
            {
                word[i] = false;
            }
        }

        public int getHowMany()
        {
            int res = 0;
            for (int i = 0; i < word.Length; i++)
                if (word[i] == true)
                    res++;
            return res;
        }

        public void negate(int which)
        {
            if (word[which])
            {
                word[which] = false;
            }
            else
            {
                word[which] = true;
            }
        }

        public void setBit(int which, bool value)
        {
            word[which] = value;
        }

        public bool getBit(int which)
        {
            return word[which];
        }

        public override string ToString()
        {
            return word.Select(x => bitToString(x))
                .Aggregate((x, y) => x + ", " + y);
        }

        public int findFirst1()
        {
            for (int i = 0; i < n; i++)
                if (word[i])
                    return i;
            throw new Exception("Nie ma zadnego elementu!");
        }

        public bool isEmpty()
        {
            for (int i = 0; i < n; i++)
                if (word[i])
                    return false;
            return true;
        }

        protected string bitToString(bool x)
        {
            if (x)
                return "1";
            else
                return "0";
        }
    }

    class GrayCode
    {
        private int _n;
        public int n
        {
            get { return _n; }
            set { _n = value; }
        }

        private int numberOfWords;

        public GrayCode()
        {
            _n = 0;
        }
        public GrayCode(int theN)
        {
            _n = theN;
        }

        private List<GrayWord> _result;
        public string[] result
        {
            get
            {
                var res = new string[numberOfWords];
                for (int i = 0; i < numberOfWords; i++)
                {
                    res[i] = _result[i].ToString();
                }
                return res;
            }
        }

        public void generateCode(int n)
        {
            GrayWord newWord;
            numberOfWords = powerOf2(n);

            _result = new List<GrayWord>();
            for (int i = 0; i < numberOfWords; i++)
            {
                //if (_result[i] == null)
                if (_result.Count() <= i)
                {
                    newWord = new GrayWord();
                    newWord.zero(n);
                    _result.Add(newWord);
                    
                }
                if (i > 0)
                    succ_next(i);
            }
            //for (int i = 1; i < numberOfWords; i++)
            //    succ_next(i);
        }

        private void succ_next(int next)
        {
            int howMany = _result[next-1].getHowMany();
            GrayWord currentWord = _result[next - 1];
            GrayWord nextWord = _result[next];

            copyPreviousWord(next);
            if (howMany % 2 == 0) //liczba jedynek parzysta 
            {
                nextWord.negate(0);
            }
            else //liczba jedynek nieparzysta 
            {
                int z;
                if (currentWord.isEmpty())
                    throw new Exception("trying to negate first 1 in empty sequence");
                else
                    z = currentWord.findFirst1();
                if (z + 1 >= n)
                    throw new Exception("z+1 > n");
                nextWord.negate(z + 1);
            }
        }

        private void copyPreviousWord(int next)
        {
            for (int i = 0; i < n; i++ )
            {
                _result[next].setBit(i, _result[next - 1].getBit(i));
            }
        }

        static public int powerOf2(int exp)
        {
            if (exp == 0)
                return 0;

            int res = 1;
            for (int i = 0; i < exp; i++)
                res *= 2;
            return res;
        }
    }
}
